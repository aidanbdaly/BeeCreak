namespace BeeCreak.Engine.Types
{
    public class State<B>(B value)
    {
        private B _value = value;

        public event Action<B>? ValueChanged;

        public B Value
        {
            get => _value;
            private set
            {
                if (EqualityComparer<B>.Default.Equals(_value, value))
                {
                    return;
                }

                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }

        public void Set(B value)
        {
            Value = value;
        }

        public void Set(Func<B , B> setStateDelegate)
        {
            Value = setStateDelegate(Value);
        }

        public void Listen<A>(State<A> a, Func<A, B> transform, out Action binding)
        {
            void handler(A a)
            {
                Value = transform(a);
            }

            a.ValueChanged += handler;

            binding = () =>
                {
                    a.ValueChanged -= handler;
                };
        }
    }

    public class ComponentBindings : Queue<Action>
    {
        public void Flush()
        {
            while (Count > 0)
            {
                Dequeue().Invoke();
            }
        }
    }
}
