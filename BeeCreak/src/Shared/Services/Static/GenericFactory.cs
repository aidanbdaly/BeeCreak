using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace BeeCreak.Shared.Services.Static;

public class GenericFactory<T, TDTO> : IGenericFactory<T, TDTO> where T : class
{
    private readonly IServiceProvider _serviceProvider;

    public GenericFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T Create(TDTO dto)
    {
        var model = _serviceProvider.GetRequiredService<T>();

        var dtoProperties = Mapper.GetPublicProperties(dto);
        var modelProperties = Mapper.GetPublicProperties(model);

        if (dtoProperties.Count != modelProperties.Count)
        {
            throw new Exception($"Public properties of {dto.GetType().Name} and {model.GetType().Name} do not match");
        }

        var zippedProperties = dtoProperties.Zip(modelProperties, (dto, obj) => (dto, obj));

        foreach (var propertyPair in zippedProperties)
        {
            var (dtoProperty, modelProperty) = propertyPair;

            if (dtoProperty.Name != modelProperty.Name)
            {
                throw new Exception("DTOs do not match");
            }

            // This runs on the assumption that we wont encounter a collection of strings or numbers etc.
            // And that every class or collection of classes will be a dto of the corresponding model

            // Collection
            if (dtoProperty.PropertyType.IsCollectible && modelProperty.PropertyType.IsCollectible)
            {
                var dtoPropertyValue = dtoProperty.GetValue(dto);
                var modelPropertyValue = modelProperty.GetValue(model);

                var dtoCollection = (System.Collections.IEnumerable)dtoPropertyValue;
                var modelCollection = (System.Collections.IList)modelPropertyValue;

                foreach (var item in dtoCollection)
                {
                    var factoryType = typeof(GenericFactory<,>)
                        .MakeGenericType(modelProperty.PropertyType.GetGenericArguments()[0], dtoProperty.PropertyType.GetGenericArguments()[0]);

                    var factory = (IGenericFactory)Activator.CreateInstance(factoryType, _serviceProvider);

                    var newModelPropertyValue = factory.Create(item);

                    modelCollection.Add(newModelPropertyValue);
                }

                modelProperty.SetValue(model, modelCollection);
            }

            // Class 
            if (dtoProperty.PropertyType.IsClass && modelProperty.PropertyType.IsClass)
            {
                var dtoPropertyValue = dtoProperty.GetValue(dto);
                var modelPropertyValue = modelProperty.GetValue(model);

                var factoryType = typeof(GenericFactory<,>)
                    .MakeGenericType(modelProperty.PropertyType, dtoProperty.PropertyType);

                var factory = (IGenericFactory)Activator.CreateInstance(factoryType, _serviceProvider);

                var newModelPropertyValue = factory.Create(dtoPropertyValue);

                modelProperty.SetValue(model, newModelPropertyValue);
            }

            modelProperty.SetValue(model, dtoProperty.GetValue(dto));
        }

        return model;
    }

    object IGenericFactory.Create(object dto)
    {
        return Create((TDTO)dto);
    }
}