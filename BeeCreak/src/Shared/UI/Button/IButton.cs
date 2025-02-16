using System;
using Microsoft.Xna.Framework;
using BeeCreak.Components;
using BeeCreak.Components.Button;

namespace BeeCreak.Shared.UI;

public interface IButton : IElement
{
    ButtonType Type { get; set; }

    ButtonVariant Variant { get; set; }

    Rectangle SourceRectangle { get; set; }

    void SetText(string text);

    void SetAction(Action action);

    void SetType(ButtonType type);

    void SetVariant(ButtonVariant variant);
}