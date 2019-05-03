﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006-2019, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2019. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-5.400)
//  Version 5.400.0.0  www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for data grid view palette content details.
    /// </summary>
    public class PaletteDataGridViewContentStates : Storage,
                                                    IPaletteContent
    {
        #region Instance Fields

        private InheritBool _draw;
        private PaletteTextHint _hint;
        private PaletteTextTrim _trim;
        private Color _color1;
        private Color _color2;
        private PaletteColorStyle _colorStyle;
        private PaletteRectangleAlign _colorAlign;
        private float _colorAngle;
        private Image _image;
        private PaletteImageStyle _imageStyle;
        private PaletteRectangleAlign _imageAlign;
        private InheritBool _multiLine;
        private PaletteRelativeAlign _multiLineH;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteDataGridViewContentStates class.
        /// </summary>
        /// <param name="inherit">Source for inheriting defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteDataGridViewContentStates(IPaletteContent inherit,
                                                NeedPaintHandler needPaint)
        {
            Debug.Assert(inherit != null);

            // Remember inheritance
            Inherit = inherit;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default the initial values
            _draw = InheritBool.Inherit;
            _hint = PaletteTextHint.Inherit;
            _trim = PaletteTextTrim.Inherit;
            _color1 = Color.Empty;
            _color2 = Color.Empty;
            _colorStyle = PaletteColorStyle.Inherit;
            _colorAlign = PaletteRectangleAlign.Inherit;
            _colorAngle = -1;
            _imageStyle = PaletteImageStyle.Inherit;
            _imageAlign = PaletteRectangleAlign.Inherit;
            _multiLine = InheritBool.Inherit;
            _multiLineH = PaletteRelativeAlign.Inherit;
           }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a property that needs syncing with the KryptonDataGridView is modified.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler SyncPropertyChanged;
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => ((Draw == InheritBool.Inherit) &&
                                           (Hint == PaletteTextHint.Inherit) &&
                                           (Trim == PaletteTextTrim.Inherit) &&
                                           (Color1 == Color.Empty) &&
                                           (Color2 == Color.Empty) &&
                                           (ColorStyle == PaletteColorStyle.Inherit) &&
                                           (ColorAlign == PaletteRectangleAlign.Inherit) &&
                                           (ColorAngle == -1) &&
                                           (Image == null) &&
                                           (ImageStyle == PaletteImageStyle.Inherit) &&
                                           (ImageAlign == PaletteRectangleAlign.Inherit) &&
                                           (MultiLine == InheritBool.Inherit) &&
                                           (MultiLineH == PaletteRelativeAlign.Inherit));

        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPaletteContent inherit)
        {
            Inherit = inherit;
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public virtual void PopulateFromBase(PaletteState state)
        {
            // Get the values and set into storage
            Draw = GetContentDraw(state);
            Hint = GetContentShortTextHint(state);
            Trim = GetContentShortTextTrim(state);
            Color1 = GetContentShortTextColor1(state);
            Color2 = GetContentShortTextColor2(state);
            ColorStyle = GetContentShortTextColorStyle(state);
            ColorAlign = GetContentShortTextColorAlign(state);
            ColorAngle = GetContentShortTextColorAngle(state);
            Image = GetContentShortTextImage(state);
            ImageStyle = GetContentShortTextImageStyle(state);
            ImageAlign = GetContentShortTextImageAlign(state);
        }
        #endregion

        #region Draw
        /// <summary>
        /// Gets a value indicating if content should be drawn.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Should content be drawn.")]
        [DefaultValue(typeof(InheritBool), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public InheritBool Draw
        {
            get => _draw;

            set
            {
                if (_draw != value)
                {
                    _draw = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the actual content draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetContentDraw(PaletteState state)
        {
            if (Draw != InheritBool.Inherit)
            {
                return Draw;
            }
            else
            {
                return Inherit.GetContentDraw(state);
            }
        }
        #endregion

        #region DrawFocus
        /// <summary>
        /// Gets the actual content draw with focus value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetContentDrawFocus(PaletteState state)
        {
            return Inherit.GetContentDrawFocus(state);
        }
        #endregion

        #region ContentImage
        /// <summary>
        /// Gets the actual content image horizontal alignment value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentImageH(PaletteState state)
        {
            return Inherit.GetContentImageH(state);
        }

        /// <summary>
        /// Gets the actual content image vertical alignment value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentImageV(PaletteState state)
        {
            return Inherit.GetContentImageV(state);
        }

        /// <summary>
        /// Gets the actual image drawing effect value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteImageEffect value.</returns>
        public PaletteImageEffect GetContentImageEffect(PaletteState state)
        {
            return Inherit.GetContentImageEffect(state);
        }

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentImageColorMap(PaletteState state)
        {
            return Inherit.GetContentImageColorMap(state);
        }

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentImageColorTo(PaletteState state)
        {
            return Inherit.GetContentImageColorTo(state);
        }
        #endregion

        #region Font
        /// <summary>
        /// Gets the actual content short text font value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public virtual Font GetContentShortTextFont(PaletteState state)
        {
            return Inherit.GetContentShortTextFont(state);
        }

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public virtual Font GetContentShortTextNewFont(PaletteState state)
        {
            return Inherit.GetContentShortTextNewFont(state);
        }
        #endregion

        #region Hint
        /// <summary>
        /// Gets the text rendering hint for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Text rendering hint for the content text.")]
        [DefaultValue(typeof(PaletteTextHint), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual PaletteTextHint Hint
        {
            get => _hint;

            set
            {
                if (value != _hint)
                {
                    _hint = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets the actual text rendering hint for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public PaletteTextHint GetContentShortTextHint(PaletteState state)
        {
            if (_hint != PaletteTextHint.Inherit)
            {
                return _hint;
            }
            else
            {
                return Inherit.GetContentShortTextHint(state);
            }
        }

        #endregion

        #region Prefix
        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state)
        {
            return Inherit.GetContentShortTextPrefix(state);
        }
        #endregion

        #region Trim
        /// <summary>
        /// Gets the text trimming for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Text trimming style for the content text.")]
        [DefaultValue(typeof(PaletteTextTrim), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual PaletteTextTrim Trim
        {
            get => _trim;

            set
            {
                if (value != _trim)
                {
                    _trim = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets the actual text trimming for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public PaletteTextTrim GetContentShortTextTrim(PaletteState state)
        {
            if (_trim != PaletteTextTrim.Inherit)
            {
                return _trim;
            }
            else
            {
                return Inherit.GetContentShortTextTrim(state);
            }
        }
        #endregion

        #region TextH
        /// <summary>
        /// Gets the actual content short text horizontal alignment value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public virtual PaletteRelativeAlign GetContentShortTextH(PaletteState state)
        {
            return Inherit.GetContentShortTextH(state);
        }
        #endregion

        #region TextV
        /// <summary>
        /// Gets the actual content short text vertical alignment value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public virtual PaletteRelativeAlign GetContentShortTextV(PaletteState state)
        {
            return Inherit.GetContentShortTextV(state);
        }
        #endregion

        #region MultiLineH
        /// <summary>
        /// Gets the relative horizontal alignment of multiline content text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Relative horizontal alignment of multiline content text.")]
        [DefaultValue(typeof(PaletteRelativeAlign), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual PaletteRelativeAlign MultiLineH
        {
            get => _multiLineH;

            set
            {
                if (value != _multiLineH)
                {
                    _multiLineH = value;
                    PerformNeedPaint();
                }
            }
        }
            
        /// <summary>
        /// Gets the actual content short text horizontal multiline alignment value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state)
        {
            if (_multiLineH != PaletteRelativeAlign.Inherit)
            {
                return _multiLineH;
            }
            else
            {
                return Inherit.GetContentShortTextMultiLineH(state);
            }
        }
        #endregion

        #region MultiLine
        /// <summary>
        /// Gets the flag indicating if multiline text is allowed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Flag indicating if multiline text is allowed..")]
        [DefaultValue(typeof(InheritBool), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual InheritBool MultiLine
        {
            get => _multiLine;

            set
            {
                if (value != _multiLine)
                {
                    _multiLine = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetContentShortTextMultiLine(PaletteState state)
        {
            if (_multiLine != InheritBool.Inherit)
            {
                return _multiLine;
            }
            else
            {
                return Inherit.GetContentShortTextMultiLine(state);
            }
        }
        #endregion

        #region Color1
        /// <summary>
        /// Gets and sets the first color for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Main color for the text.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual Color Color1
        {
            get => _color1;

            set
            {
                if (value != _color1)
                {
                    _color1 = value;
                    OnSyncPropertyChanged(EventArgs.Empty);
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the first color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentShortTextColor1(PaletteState state)
        {
            if (_color1 != Color.Empty)
            {
                return _color1;
            }
            else
            {
                return Inherit.GetContentShortTextColor1(state);
            }
        }
        #endregion

        #region Color2
        /// <summary>
        /// Gets and sets the second color for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Secondary color for the text.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual Color Color2
        {
            get => _color2;

            set
            {
                if (value != _color2)
                {
                    _color2 = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentShortTextColor2(PaletteState state)
        {
            if (_color2 != Color.Empty)
            {
                return _color2;
            }
            else
            {
                return Inherit.GetContentShortTextColor2(state);
            }
        }
        #endregion

        #region ColorStyle
        /// <summary>
        /// Gets and sets the color drawing style for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Color drawing style for the text.")]
        [DefaultValue(typeof(PaletteColorStyle), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual PaletteColorStyle ColorStyle
        {
            get => _colorStyle;

            set
            {
                if (value != _colorStyle)
                {
                    _colorStyle = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetContentShortTextColorStyle(PaletteState state)
        {
            if (_colorStyle != PaletteColorStyle.Inherit)
            {
                return _colorStyle;
            }
            else
            {
                return Inherit.GetContentShortTextColorStyle(state);
            }
        }
        #endregion

        #region ColorAlign
        /// <summary>
        /// Gets and set the color alignment for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Color alignment style for the text.")]
        [DefaultValue(typeof(PaletteRectangleAlign), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual PaletteRectangleAlign ColorAlign
        {
            get => _colorAlign;

            set
            {
                if (value != _colorAlign)
                {
                    _colorAlign = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the color alignment style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state)
        {
            if (_colorAlign != PaletteRectangleAlign.Inherit)
            {
                return _colorAlign;
            }
            else
            {
                return Inherit.GetContentShortTextColorAlign(state);
            }
        }
        #endregion

        #region ColorAngle
        /// <summary>
        /// Gets and sets the color angle for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Color angle for the text.")]
        [DefaultValue(-1f)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual float ColorAngle
        {
            get => _colorAngle;

            set
            {
                if (value != _colorAngle)
                {
                    _colorAngle = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the color angle for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetContentShortTextColorAngle(PaletteState state)
        {
            if (_colorAngle != -1)
            {
                return _colorAngle;
            }
            else
            {
                return Inherit.GetContentShortTextColorAngle(state);
            }
        }
        #endregion

        #region Image
        /// <summary>
        /// Gets and sets the image for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for the text.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual Image Image
        {
            get => _image;

            set
            {
                if (value != _image)
                {
                    _image = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets an image for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetContentShortTextImage(PaletteState state)
        {
            if (_image != null)
            {
                return _image;
            }
            else
            {
                return Inherit.GetContentShortTextImage(state);
            }
        }
        #endregion

        #region ImageStyle
        /// <summary>
        /// Gets and sets the image style for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image style for the text.")]
        [DefaultValue(typeof(PaletteImageStyle), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual PaletteImageStyle ImageStyle
        {
            get => _imageStyle;

            set
            {
                if (value != _imageStyle)
                {
                    _imageStyle = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the image style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetContentShortTextImageStyle(PaletteState state)
        {
            if (_imageStyle != PaletteImageStyle.Inherit)
            {
                return _imageStyle;
            }
            else
            {
                return Inherit.GetContentShortTextImageStyle(state);
            }
        }
        #endregion

        #region ImageAlign
        /// <summary>
        /// Gets and set the image alignment for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image alignment style for the text.")]
        [DefaultValue(typeof(PaletteRectangleAlign), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual PaletteRectangleAlign ImageAlign
        {
            get => _imageAlign;

            set
            {
                if (value != _imageAlign)
                {
                    _imageAlign = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the image alignment style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state)
        {
            if (_imageAlign != PaletteRectangleAlign.Inherit)
            {
                return _imageAlign;
            }
            else
            {
                return Inherit.GetContentShortTextImageAlign(state);
            }
        }
        #endregion

        #region ContentLongText
        /// <summary>
        /// Gets the actual content long text font value.
        /// </summary>
        /// <returns>Font value.</returns>
        /// <param name="state">Palette value should be applicable to this state.</param>
        public Font GetContentLongTextFont(PaletteState state)
        {
            return Inherit.GetContentLongTextFont(state);
        }

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetContentLongTextNewFont(PaletteState state)
        {
            return Inherit.GetContentLongTextNewFont(state);
        }

        /// <summary>
        /// Gets the actual text rendering hint for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public PaletteTextHint GetContentLongTextHint(PaletteState state)
        {
            return Inherit.GetContentLongTextHint(state);
        }

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state)
        {
            return Inherit.GetContentLongTextPrefix(state);
        }
        
        /// <summary>
        /// Gets the actual text trimming for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public PaletteTextTrim GetContentLongTextTrim(PaletteState state)
        {
            return Inherit.GetContentLongTextTrim(state);
        }

        /// <summary>
        /// Gets the actual content long text horizontal alignment value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentLongTextH(PaletteState state)
        {
            return Inherit.GetContentLongTextH(state);
        }

        /// <summary>
        /// Gets the actual content long text vertical alignment value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentLongTextV(PaletteState state)
        {
            return Inherit.GetContentLongTextV(state);
        }

        /// <summary>
        /// Gets the actual content long text horizontal multiline alignment value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state)
        {
            return Inherit.GetContentLongTextMultiLineH(state);
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetContentLongTextMultiLine(PaletteState state)
        {
            return Inherit.GetContentLongTextMultiLine(state);
        }

        /// <summary>
        /// Gets the first color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentLongTextColor1(PaletteState state)
        {
            return Inherit.GetContentLongTextColor1(state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentLongTextColor2(PaletteState state)
        {
            return Inherit.GetContentLongTextColor2(state);
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetContentLongTextColorStyle(PaletteState state)
        {
            return Inherit.GetContentLongTextColorStyle(state);
        }

        /// <summary>
        /// Gets the color alignment style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state)
        {
            return Inherit.GetContentLongTextColorAlign(state);
        }

        /// <summary>
        /// Gets the color angle for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetContentLongTextColorAngle(PaletteState state)
        {
            return Inherit.GetContentLongTextColorAngle(state);
        }

        /// <summary>
        /// Gets an image for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetContentLongTextImage(PaletteState state)
        {
            return Inherit.GetContentLongTextImage(state);
        }

        /// <summary>
        /// Gets the image style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetContentLongTextImageStyle(PaletteState state)
        {
            return Inherit.GetContentLongTextImageStyle(state);
        }

        /// <summary>
        /// Gets the image alignment style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state)
        {
            return Inherit.GetContentLongTextImageAlign(state);
        }
        #endregion

        #region Padding
        /// <summary>
        /// Gets the actual padding between the border and content drawing.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Padding value.</returns>
        public virtual Padding GetContentPadding(PaletteState state)
        {
            return Inherit.GetContentPadding(state);
        }
        #endregion

        #region AdjacentGap
        /// <summary>
        /// Gets the actual padding between adjacent content items.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer value.</returns>
        public int GetContentAdjacentGap(PaletteState state)
        {
            return Inherit.GetContentAdjacentGap(state);
        }
        #endregion

        #region ContentStyle
        /// <summary>
        /// Gets the style appropriate for this content.
        /// </summary>
        /// <returns>Content style.</returns>
        public PaletteContentStyle GetContentStyle()
        {
            return Inherit.GetContentStyle();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the inheritence parent.
        /// </summary>
        protected IPaletteContent Inherit { get; private set; }

        /// <summary>
        /// Raises the SyncPropertyChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnSyncPropertyChanged(EventArgs e)
        {
            SyncPropertyChanged?.Invoke(this, e);
        }
        #endregion
    }
}
