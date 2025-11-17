using System.Runtime.Intrinsics.X86;

namespace BeeCreak.Core.State
{
    public sealed class Binding(Action releaseCallback) : IDisposable
    {
        public void Dispose() => releaseCallback();
    }

    public class State<B>(B value)
    {
        private B _value = value;

        public event Action<B>? ValueChanged;

        public B Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<B>.Default.Equals(_value, value))
                {
                    return;
                }

                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }

        // Perhaps hold 'sources internally

        public Action Listen<A>(State<A> a, Func<A, B> transform)
        {
            return Bind<A>(a, this, transform);   
        }

        private static Action Bind<A>(State<A> a, State<B> b, Func<A, B> transform)
        {
            void handler(A a)
            {
                b.Value = transform(a);
            }

            a.ValueChanged += handler;

            return () =>
            {
                a.ValueChanged -= handler;
            };
        }
    }

    public class ActionBuffer : Queue<Action>
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
