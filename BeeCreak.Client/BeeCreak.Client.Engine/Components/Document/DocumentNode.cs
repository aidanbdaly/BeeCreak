using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Services.Layout
{
    public sealed class DocumentNode
    {
        private readonly List<DocumentNode> children = [];

        public DocumentNode? Parent { get; private set; }

        public IReadOnlyList<DocumentNode> Children => children;

        public LayoutStyle Style { get; } = new();

        public bool Visible { get; private set; } = true;

        public Texture2D? Texture { get; private set; }

        public Rectangle? Source { get; private set; }

        public SpriteFont? Font { get; private set; }

        public string? Text { get; private set; }

        public Color Color { get; private set; } = Color.White;

        public Color? FillColor { get; private set; }

        public float Opacity { get; private set; } = 1f;

        public float Rotation { get; private set; }

        public Vector2 Origin { get; private set; }

        public Vector2 Scale { get; private set; } = Vector2.One;

        public SpriteEffects Effects { get; private set; } = SpriteEffects.None;

        public float LayerDepth { get; private set; }

        public Action<DocumentNode>? OnHoverStart { get; private set; }

        public Action<DocumentNode>? OnHoverEnd { get; private set; }

        public Action<DocumentNode>? OnClick { get; private set; }

        internal LayoutRect LayoutRect { get; set; }

        internal Vector2 MeasuredSize { get; set; }

        internal bool IsDirty { get; set; } = true;

        public bool IsHovered { get; set; } = false;

        public static DocumentNode Create() => new();

        private DocumentNode Root => Parent == null ? this : Parent.Root;

        public DocumentNode AddChild(DocumentNode child)
        {
            child.Parent = this;
            children.Add(child);
            MarkDirty();
            return child;
        }

        public DocumentNode AddChild() => AddChild(new DocumentNode());

        public DocumentNode End() => Parent ?? this;

        public DocumentNode ClearChildren()
        {
            children.Clear();
            MarkDirty();
            return this;
        }

        public DocumentNode SetVisible(bool visible = true)
        {
            Visible = visible;
            MarkDirty();
            return this;
        }

        public DocumentNode SetWidth(LayoutValue value)
        {
            Style.Width = value;
            MarkDirty();
            return this;
        }

        public DocumentNode SetWidth(float pixels) => SetWidth(LayoutValue.Px(pixels));

        public DocumentNode SetWidthPercent(float percent) => SetWidth(LayoutValue.Percent(percent));

        public DocumentNode SetHeight(LayoutValue value)
        {
            Style.Height = value;
            MarkDirty();
            return this;
        }

        public DocumentNode SetHeight(float pixels) => SetHeight(LayoutValue.Px(pixels));

        public DocumentNode SetHeightPercent(float percent) => SetHeight(LayoutValue.Percent(percent));

        public DocumentNode SetSize(float widthPixels, float heightPixels)
        {
            Style.Width = LayoutValue.Px(widthPixels);
            Style.Height = LayoutValue.Px(heightPixels);
            MarkDirty();
            return this;
        }

        public DocumentNode SetSizePercent(float widthPercent, float heightPercent)
        {
            Style.Width = LayoutValue.Percent(widthPercent);
            Style.Height = LayoutValue.Percent(heightPercent);
            MarkDirty();
            return this;
        }

        public DocumentNode SetPadding(float all)
        {
            Style.Padding = LayoutBox.All(all);
            MarkDirty();
            return this;
        }

        public DocumentNode SetPadding(float horizontal, float vertical)
        {
            Style.Padding = LayoutBox.Symmetric(horizontal, vertical);
            MarkDirty();
            return this;
        }

        public DocumentNode SetPadding(float left, float top, float right, float bottom)
        {
            Style.Padding = new LayoutBox(left, top, right, bottom);
            MarkDirty();
            return this;
        }

        public DocumentNode SetMargin(float all)
        {
            Style.Margin = LayoutBox.All(all);
            MarkDirty();
            return this;
        }

        public DocumentNode SetMargin(float horizontal, float vertical)
        {
            Style.Margin = LayoutBox.Symmetric(horizontal, vertical);
            MarkDirty();
            return this;
        }

        public DocumentNode SetMargin(float left, float top, float right, float bottom)
        {
            Style.Margin = new LayoutBox(left, top, right, bottom);
            MarkDirty();
            return this;
        }

        public DocumentNode SetDirection(LayoutDirection direction)
        {
            Style.Direction = direction;
            MarkDirty();
            return this;
        }

        public DocumentNode SetAlign(LayoutAlign align)
        {
            Style.Align = align;
            MarkDirty();
            return this;
        }

        public DocumentNode SetJustify(LayoutJustify justify)
        {
            Style.Justify = justify;
            MarkDirty();
            return this;
        }

        public DocumentNode SetGap(float gap)
        {
            Style.Gap = gap;
            MarkDirty();
            return this;
        }

        public DocumentNode SetPosition(LayoutPosition position)
        {
            Style.Position = position;
            MarkDirty();
            return this;
        }

        public DocumentNode SetAbsolute(float left, float top)
        {
            Style.Position = LayoutPosition.Absolute;
            Style.Left = LayoutValue.Px(left);
            Style.Top = LayoutValue.Px(top);
            MarkDirty();
            return this;
        }

        public DocumentNode SetAbsolutePercent(float leftPercent, float topPercent)
        {
            Style.Position = LayoutPosition.Absolute;
            Style.Left = LayoutValue.Percent(leftPercent);
            Style.Top = LayoutValue.Percent(topPercent);
            MarkDirty();
            return this;
        }

        public DocumentNode SetLeft(float pixels)
        {
            Style.Left = LayoutValue.Px(pixels);
            Style.Position = LayoutPosition.Absolute;
            MarkDirty();
            return this;
        }

        public DocumentNode SetLeftPercent(float percent)
        {
            Style.Left = LayoutValue.Percent(percent);
            Style.Position = LayoutPosition.Absolute;
            MarkDirty();
            return this;
        }

        public DocumentNode SetTop(float pixels)
        {
            Style.Top = LayoutValue.Px(pixels);
            Style.Position = LayoutPosition.Absolute;
            MarkDirty();
            return this;
        }

        public DocumentNode SetTopPercent(float percent)
        {
            Style.Top = LayoutValue.Percent(percent);
            Style.Position = LayoutPosition.Absolute;
            MarkDirty();
            return this;
        }

        public DocumentNode SetRight(float pixels)
        {
            Style.Right = LayoutValue.Px(pixels);
            Style.Position = LayoutPosition.Absolute;
            MarkDirty();
            return this;
        }

        public DocumentNode SetRightPercent(float percent)
        {
            Style.Right = LayoutValue.Percent(percent);
            Style.Position = LayoutPosition.Absolute;
            MarkDirty();
            return this;
        }

        public DocumentNode SetBottom(float pixels)
        {
            Style.Bottom = LayoutValue.Px(pixels);
            Style.Position = LayoutPosition.Absolute;
            MarkDirty();
            return this;
        }

        public DocumentNode SetBottomPercent(float percent)
        {
            Style.Bottom = LayoutValue.Percent(percent);
            Style.Position = LayoutPosition.Absolute;
            MarkDirty();
            return this;
        }

        public DocumentNode SetText(string text, SpriteFont font, Color? color = null)
        {
            Text = text;
            Font = font;
            Texture = null;
            Source = null;
            if (color.HasValue)
            {
                Color = color.Value;
            }

            MarkDirty();
            return this;
        }

        public DocumentNode SetTexture(Texture2D texture, Rectangle? source = null, Color? color = null)
        {
            Texture = texture;
            Source = source;
            Text = null;
            Font = null;
            if (color.HasValue)
            {
                Color = color.Value;
            }

            MarkDirty();
            return this;
        }

        public DocumentNode SetColor(Color color)
        {
            Color = color;
            MarkDirty();
            return this;
        }

        public DocumentNode SetFill(Color color)
        {
            FillColor = color;
            MarkDirty();
            return this;
        }

        public DocumentNode ClearFill()
        {
            FillColor = null;
            MarkDirty();
            return this;
        }

        public DocumentNode SetOpacity(float opacity)
        {
            Opacity = opacity;
            MarkDirty();
            return this;
        }

        public DocumentNode SetRotation(float rotation)
        {
            Rotation = rotation;
            MarkDirty();
            return this;
        }

        public DocumentNode SetOrigin(Vector2 origin)
        {
            Origin = origin;
            MarkDirty();
            return this;
        }

        public DocumentNode SetScale(float scale)
        {
            Scale = new Vector2(scale);
            MarkDirty();
            return this;
        }

        public DocumentNode SetScale(Vector2 scale)
        {
            Scale = scale;
            MarkDirty();
            return this;
        }

        public DocumentNode SetEffects(SpriteEffects effects)
        {
            Effects = effects;
            MarkDirty();
            return this;
        }

        public DocumentNode SetLayerDepth(float layerDepth)
        {
            LayerDepth = layerDepth;
            MarkDirty();
            return this;
        }

        public DocumentNode SetOnHoverStart(Action<DocumentNode> onHoverStart)
        {
            OnHoverStart = onHoverStart;
            return this;
        }

        public DocumentNode SetOnHoverEnd(Action<DocumentNode> onHoverEnd)
        {
            OnHoverEnd = onHoverEnd;
            return this;
        }

        public DocumentNode SetOnClick(Action<DocumentNode> onClick)
        {
            OnClick = onClick;
            return this;
        }

        public void MarkDirty()
        {
            Root.IsDirty = true;
        }
    }
}
