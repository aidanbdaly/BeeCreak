namespace BeeCreak
{
    using System;

    public class Mode<T>
        where T : Enum
    {
        public Mode(T initialMode)
        {
            Current = initialMode;
        }

        public T Current { get; set; }

        public void Switch(T mode)
        {
            Current = mode;
        }
    }
}
