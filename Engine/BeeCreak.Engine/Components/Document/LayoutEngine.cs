using System;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services.Layout
{
    // DocumentEngine
    public static class LayoutEngine
    {
        public static RenderNode Build(DocumentNode root, Rectangle bounds)
        {
            var available = new Vector2(bounds.Width, bounds.Height);
            Measure(root, available, forceSize: true);
            Layout(root, new LayoutRect(bounds.X, bounds.Y, root.MeasuredSize.X, root.MeasuredSize.Y));
            root.IsDirty = false;
            return BuildComponent(root);
        }

        private static Vector2 Measure(DocumentNode node, Vector2 available, bool forceSize)
        {
            var padding = node.Style.Padding;
            var width = node.Style.Width.Resolve(available.X);
            var height = node.Style.Height.Resolve(available.Y);

            var availableForChildren = new Vector2(
                MathF.Max(0f, (float.IsNaN(width) ? available.X : width) - padding.Horizontal),
                MathF.Max(0f, (float.IsNaN(height) ? available.Y : height) - padding.Vertical));

            var contentSize = MeasureContent(node);
            var childrenSize = MeasureChildren(node, availableForChildren);

            var autoWidth = MathF.Max(contentSize.X, childrenSize.X) + padding.Horizontal;
            var autoHeight = MathF.Max(contentSize.Y, childrenSize.Y) + padding.Vertical;

            if (float.IsNaN(width))
            {
                width = autoWidth;
            }

            if (float.IsNaN(height))
            {
                height = autoHeight;
            }

            if (forceSize)
            {
                if (node.Style.Width.IsAuto)
                {
                    width = available.X;
                }

                if (node.Style.Height.IsAuto)
                {
                    height = available.Y;
                }
            }

            width = MathF.Max(0f, width);
            height = MathF.Max(0f, height);

            node.MeasuredSize = new Vector2(width, height);
            return node.MeasuredSize;
        }

        private static Vector2 MeasureContent(DocumentNode node)
        {
            var contentWidth = 0f;
            var contentHeight = 0f;

            if (node.Text != null && node.Font != null)
            {
                var size = node.Font.MeasureString(node.Text) * node.Scale;
                contentWidth = MathF.Max(contentWidth, size.X);
                contentHeight = MathF.Max(contentHeight, size.Y);
            }

            if (node.Texture != null)
            {
                var source = node.Source;
                var width = source?.Width ?? node.Texture.Width;
                var height = source?.Height ?? node.Texture.Height;
                contentWidth = MathF.Max(contentWidth, width * node.Scale.X);
                contentHeight = MathF.Max(contentHeight, height * node.Scale.Y);
            }

            return new Vector2(contentWidth, contentHeight);
        }

        private static Vector2 MeasureChildren(DocumentNode node, Vector2 availableForChildren)
        {
            var count = 0;
            var mainTotal = 0f;
            var crossMax = 0f;
            var direction = node.Style.Direction;

            foreach (var child in node.Children)
            {
                Measure(child, availableForChildren, forceSize: false);

                if (child.Style.Position == LayoutPosition.Absolute)
                {
                    continue;
                }

                count++;
                var margin = child.Style.Margin;

                if (direction == LayoutDirection.Row)
                {
                    mainTotal += margin.Left + child.MeasuredSize.X + margin.Right;
                    crossMax = MathF.Max(crossMax, margin.Top + child.MeasuredSize.Y + margin.Bottom);
                }
                else
                {
                    mainTotal += margin.Top + child.MeasuredSize.Y + margin.Bottom;
                    crossMax = MathF.Max(crossMax, margin.Left + child.MeasuredSize.X + margin.Right);
                }
            }

            if (count > 1)
            {
                mainTotal += (count - 1) * node.Style.Gap;
            }

            return direction == LayoutDirection.Row
                ? new Vector2(mainTotal, crossMax)
                : new Vector2(crossMax, mainTotal);
        }

        private static void Layout(DocumentNode node, LayoutRect rect)
        {
            node.LayoutRect = rect;
            var padding = node.Style.Padding;
            var contentX = rect.X + padding.Left;
            var contentY = rect.Y + padding.Top;
            var contentWidth = MathF.Max(0f, rect.Width - padding.Horizontal);
            var contentHeight = MathF.Max(0f, rect.Height - padding.Vertical);

            LayoutFlowChildren(node, contentX, contentY, contentWidth, contentHeight);
            LayoutAbsoluteChildren(node, contentX, contentY, contentWidth, contentHeight);
        }

        private static void LayoutFlowChildren(DocumentNode node, float contentX, float contentY, float contentWidth, float contentHeight)
        {
            var direction = node.Style.Direction;
            var align = node.Style.Align;
            var justify = node.Style.Justify;
            var gap = node.Style.Gap;

            var flowChildren = new DocumentNode[node.Children.Count];
            var flowCount = 0;
            for (var i = 0; i < node.Children.Count; i++)
            {
                var child = node.Children[i];
                if (child.Style.Position == LayoutPosition.Relative)
                {
                    flowChildren[flowCount++] = child;
                }
            }

            if (flowCount == 0)
            {
                return;
            }

            var mainTotal = 0f;

            for (var i = 0; i < flowCount; i++)
            {
                var child = flowChildren[i];
                var margin = child.Style.Margin;
                if (direction == LayoutDirection.Row)
                {
                    mainTotal += margin.Left + child.MeasuredSize.X + margin.Right;
                }
                else
                {
                    mainTotal += margin.Top + child.MeasuredSize.Y + margin.Bottom;
                }
            }

            if (flowCount > 1)
            {
                mainTotal += (flowCount - 1) * gap;
            }

            var availableMain = direction == LayoutDirection.Row ? contentWidth : contentHeight;
            var freeSpace = availableMain - mainTotal;

            var spacing = gap;
            var initialOffset = 0f;

            switch (justify)
            {
                case LayoutJustify.Center:
                    initialOffset = freeSpace / 2f;
                    break;
                case LayoutJustify.End:
                    initialOffset = freeSpace;
                    break;
                case LayoutJustify.SpaceBetween:
                    if (flowCount > 1 && freeSpace > 0f)
                    {
                        spacing += freeSpace / (flowCount - 1);
                    }
                    break;
                case LayoutJustify.SpaceAround:
                    if (flowCount > 0 && freeSpace > 0f)
                    {
                        var extra = freeSpace / flowCount;
                        spacing += extra;
                        initialOffset = extra / 2f;
                    }
                    break;
            }

            var cursor = (direction == LayoutDirection.Row ? contentX : contentY) + initialOffset;

            for (var i = 0; i < flowCount; i++)
            {
                var child = flowChildren[i];
                var margin = child.Style.Margin;

                var childWidth = child.MeasuredSize.X;
                var childHeight = child.MeasuredSize.Y;

                if (direction == LayoutDirection.Row)
                {
                    cursor += margin.Left;

                    if (align == LayoutAlign.Stretch && child.Style.Height.IsAuto)
                    {
                        childHeight = MathF.Max(0f, contentHeight - margin.Vertical);
                    }

                    var crossOffset = align switch
                    {
                        LayoutAlign.Center => (contentHeight - margin.Vertical - childHeight) / 2f,
                        LayoutAlign.End => contentHeight - margin.Vertical - childHeight,
                        _ => 0f
                    };

                    var x = cursor;
                    var y = contentY + margin.Top + crossOffset;

                    Layout(child, new LayoutRect(x, y, childWidth, childHeight));

                    cursor += childWidth + margin.Right;
                    if (i < flowCount - 1)
                    {
                        cursor += spacing;
                    }
                }
                else
                {
                    cursor += margin.Top;

                    if (align == LayoutAlign.Stretch && child.Style.Width.IsAuto)
                    {
                        childWidth = MathF.Max(0f, contentWidth - margin.Horizontal);
                    }

                    var crossOffset = align switch
                    {
                        LayoutAlign.Center => (contentWidth - margin.Horizontal - childWidth) / 2f,
                        LayoutAlign.End => contentWidth - margin.Horizontal - childWidth,
                        _ => 0f
                    };

                    var x = contentX + margin.Left + crossOffset;
                    var y = cursor;

                    Layout(child, new LayoutRect(x, y, childWidth, childHeight));

                    cursor += childHeight + margin.Bottom;
                    if (i < flowCount - 1)
                    {
                        cursor += spacing;
                    }
                }
            }
        }

        private static void LayoutAbsoluteChildren(DocumentNode node, float contentX, float contentY, float contentWidth, float contentHeight)
        {
            foreach (var child in node.Children)
            {
                if (child.Style.Position != LayoutPosition.Absolute)
                {
                    continue;
                }

                var margin = child.Style.Margin;
                var left = ResolveOffset(child.Style.Left, contentWidth);
                var right = ResolveOffset(child.Style.Right, contentWidth);
                var top = ResolveOffset(child.Style.Top, contentHeight);
                var bottom = ResolveOffset(child.Style.Bottom, contentHeight);

                var childWidth = child.MeasuredSize.X;
                var childHeight = child.MeasuredSize.Y;

                if (left.HasValue && right.HasValue && child.Style.Width.IsAuto)
                {
                    childWidth = MathF.Max(0f, contentWidth - left.Value - right.Value - margin.Horizontal);
                }

                if (top.HasValue && bottom.HasValue && child.Style.Height.IsAuto)
                {
                    childHeight = MathF.Max(0f, contentHeight - top.Value - bottom.Value - margin.Vertical);
                }

                var x = contentX + (left ?? (right.HasValue ? contentWidth - right.Value - childWidth : 0f)) + margin.Left;
                var y = contentY + (top ?? (bottom.HasValue ? contentHeight - bottom.Value - childHeight : 0f)) + margin.Top;

                Layout(child, new LayoutRect(x, y, childWidth, childHeight));
            }
        }

        private static float? ResolveOffset(LayoutValue? value, float available)
        {
            if (!value.HasValue)
            {
                return null;
            }

            if (value.Value.Unit == LayoutUnit.Auto)
            {
                return null;
            }

            return value.Value.Resolve(available);
        }

        private static RenderNode BuildComponent(DocumentNode node)
        {
            var component = new RenderNode(node);

            foreach (var child in node.Children)
            {
                component.Children.Add(BuildComponent(child));
            }

            return component;
        }
    }
}
