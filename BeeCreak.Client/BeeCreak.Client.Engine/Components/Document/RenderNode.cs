using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Services.Layout
{
    public sealed class RenderNode(DocumentNode node)
    {
        public Texture2D? Texture { get; } = node.Texture;

        public SpriteFont? Font { get; } = node.Font;

        public string? Text { get; } = node.Text;

        public Rectangle Destination { get; } = node.LayoutRect.ToRectangle();

        public Rectangle? Source { get; } = node.Source;

        public Color Color { get; } = node.Color;

        public Color? FillColor { get; } = node.FillColor;

        public float Opacity { get; } = node.Opacity;

        public float Rotation { get; } = node.Rotation;

        public Vector2 Origin { get; } = node.Origin;

        public Vector2 Scale { get; } = node.Scale;

        public SpriteEffects Effects { get; } = node.Effects;

        public float LayerDepth { get; } = node.LayerDepth;

        public bool Visible { get; } = node.Visible;

        public bool IsHovered => node.IsHovered;

        public Action<DocumentNode>? OnHoverStart { get; } = node.OnHoverStart;

        public Action<DocumentNode>? OnHoverEnd { get; } = node.OnHoverEnd;

        public Action<DocumentNode>? OnClick { get; } = node.OnClick;

        public List<RenderNode> Children { get; } = [];

        public void HoverStart()
        {
            node.IsHovered = true;
            OnHoverStart?.Invoke(node);
        }

        public void HoverEnd()
        {
            node.IsHovered = false;
            OnHoverEnd?.Invoke(node);
        }

        public void Click()
        {
            OnClick?.Invoke(node);
        }
    }
}
