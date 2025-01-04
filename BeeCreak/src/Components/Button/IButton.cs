using System;
using BeeCreak.Components;
using BeeCreak.Components.Button;

namespace BeeCreak.UI.Components;

public interface IButton : IElement
{
    ButtonType Type { get; set; }

    ButtonVariant Variant { get; set; }

    void SetText(string text);

    void SetAction(Action action);
}