using Raylib_CsLo;
using System.Numerics;

namespace csElectricFieldSimulator
{
    public unsafe class Gui
    {
        // csElectricFieldSimulator: controls initialization
        //----------------------------------------------------------------------------------
        // Define controls variables
        bool settingsWindowBoxActive = true;            // WindowBox: settingsWindowBox
        bool probesPerChargeSpinnerEditMode = false;
        public int probesPerChargeSpinnerValue = 25;            // Spinner: probesPerChargeSpinner
        bool lineThicknessSpinnerEditMode = false;
        public int lineThicknessSpinnerValue = 1;            // Spinner: lineThicknessSpinner
        bool qualitySpinnerEditMode = false;
        public int qualitySpinnerValue = 2;            // Spinner: qualitySpinner
        bool lodQualitySpinnerEditMode = false;
        public int lodQualitySpinnerValue = 10;            // Spinner: lodQualitySpinner
        bool probeRadiusSpinnerEditMode = false;
        public int probeRadiusSpinnerValue = 25;            // Spinner: probeRadiusSpinner
        bool directionVisBoxEditMode = false;
        public int directionVisBoxActive = 0;            // DropdownBox: directionVisBox

        // states
        public bool showTools = false;
        public bool showSettings = false;
        public bool chargePolarity = false; // false = negative ; true = postive
        public bool dragMoveMode = false;
        public bool addChargeMode = false;
        public bool eraseMode = false;
        public bool editChargeMode = false;
        public bool zoomMode = false;

        // options
        public bool setChargeToZero = false;
        public bool clearCharges = false;
        public bool showLines = true;
        public bool showDots = false;

        // Define controls rectangles
        Rectangle[] layoutRecs = new Rectangle[29]{
            new Rectangle( 80, 24, 48, 48),    // Button: settingsButton
            new Rectangle( 24, 24, 48, 48 ),    // Button: toolButton
            new Rectangle( 24, 96, 48, 24 ),    // Button: addButton
            new Rectangle( 24, 128, 48, 24 ),    // Button: removeButton
            new Rectangle( 24, 192, 48, 24 ),    // Button: dragMoveButton
            new Rectangle( 24, 160, 48, 24 ),    // Button: editChargeButton
            new Rectangle( 80, 128, 48, 24 ),    // Button: RemoveButtonClear
            new Rectangle( 80, 160, 48, 24 ),    // Button: editChargeButtonSetZero
            new Rectangle( 136, 24, 272, 328 ),    // WindowBox: settingsWindowBox
            new Rectangle( 24, 224, 48, 24 ),    // Button: zoomButton
            new Rectangle( 144, 64, 256, 120 ),    // GroupBox: GroupBox012
            new Rectangle( 272, 80, 112, 16 ),    // Label: Label015
            new Rectangle( 272, 104, 120, 16 ),    // Label: Label016
            new Rectangle( 152, 152, 120, 16 ),    // Spinner: probesPerChargeSpinner
            new Rectangle( 272, 152, 120, 16 ),    // Label: Label018
            new Rectangle( 272, 128, 120, 16 ),    // Label: Label020
            new Rectangle( 144, 200, 256, 144 ),    // GroupBox: GroupBox021
            new Rectangle( 152, 216, 112, 16 ),    // Spinner: lineThicknessSpinner
            new Rectangle( 264, 216, 120, 16 ),    // Label: Label023
            new Rectangle( 152, 80, 120, 16 ),    // Spinner: qualitySpinner
            new Rectangle( 152, 104, 120, 16 ),    // Spinner: lodQualitySpinner
            new Rectangle( 152, 128, 120, 16 ),    // Spinner: probeRadiusSpinner
            new Rectangle( 152, 240, 112, 24 ),    // DropdownBox: directionVisBox
            new Rectangle( 264, 240, 120, 24 ),    // Label: Label027
            new Rectangle( 24, 256, 48, 24 ),    // Button: hideToolsButton
            new Rectangle( 152, 272, 112, 24 ),    // Button: showLinesButton
            new Rectangle( 152, 304, 112, 24 ),    // Button: showDotsButton
            new Rectangle( 80, 96, 24, 24 ),    // Button: addButtonPositive
            new Rectangle( 104, 96, 24, 24 ),    // Button: addButtonNegative
        };

        Rectangle[] mainLayoutRecs = new Rectangle[]
        {
            new Rectangle( 80, 24, 48, 48),    // Button: settingsButton
            new Rectangle( 24, 24, 48, 48 ),    // Button: toolButton
        };

        Rectangle[] toolLayoutRecs = new Rectangle[]
        {
            new Rectangle( 24, 96, 48, 24 ),    // Button: addButton
            new Rectangle( 24, 128, 48, 24 ),    // Button: removeButton
            new Rectangle( 24, 192, 48, 24 ),    // Button: dragMoveButton
            new Rectangle( 24, 160, 48, 24 ),    // Button: editChargeButton
            new Rectangle( 80, 128, 48, 24 ),    // Button: RemoveButtonClear
            new Rectangle( 80, 160, 48, 24 ),    // Button: editChargeButtonSetZero
            new Rectangle( 80, 96, 24, 24 ),    // Button: addButtonPositive
            new Rectangle( 104, 96, 24, 24 ),    // Button: addButtonNegative
            new Rectangle( 24, 224, 48, 24 ),    // Button: zoomButton
        };

        Rectangle[] settingsLayoutRecs = new Rectangle[]
        {
            new Rectangle( 136, 24, 272, 328 ),    // WindowBox: settingsWindowBox
            new Rectangle( 144, 64, 256, 120 ),    // GroupBox: GroupBox012
            new Rectangle( 272, 80, 112, 16 ),    // Label: Label015
            new Rectangle( 272, 104, 120, 16 ),    // Label: Label016
            new Rectangle( 152, 152, 120, 16 ),    // Spinner: probesPerChargeSpinner
            new Rectangle( 272, 152, 120, 16 ),    // Label: Label018
            new Rectangle( 272, 128, 120, 16 ),    // Label: Label020
            new Rectangle( 144, 200, 256, 144 ),    // GroupBox: GroupBox021
            new Rectangle( 152, 216, 112, 16 ),    // Spinner: lineThicknessSpinner
            new Rectangle( 264, 216, 120, 16 ),    // Label: Label023
            new Rectangle( 152, 80, 120, 16 ),    // Spinner: qualitySpinner
            new Rectangle( 152, 104, 120, 16 ),    // Spinner: lodQualitySpinner
            new Rectangle( 152, 128, 120, 16 ),    // Spinner: probeRadiusSpinner
            new Rectangle( 152, 240, 112, 24 ),    // DropdownBox: directionVisBox
            new Rectangle( 264, 240, 120, 24 ),    // Label: Label027
            new Rectangle( 152, 272, 112, 24 ),    // Button: showLinesButton
            new Rectangle( 152, 304, 112, 24 ),    // Button: showDotsButton
        };
        //----------------------------------------------------------------------------------


        //--------------------------------------------------------------------------------------

        public (int probesPerCharge, int probeRadius, int lodQuality, int quality) getSettings()
        {
            return (
                probesPerCharge: probesPerChargeSpinnerValue,
                probeRadius: probeRadiusSpinnerValue,
                lodQuality: lodQualitySpinnerValue,
                quality: qualitySpinnerValue
            );
        }


        // i wrote this with the worst headache i've ever had
        // TODO: Refactor
        public bool isMouseOnControls(Vector2 mousePos)
        {
            bool result = false;

            if (mainLayoutRecs.Any(a => isMouseInRectanlge(a, mousePos) == true))
            {
                result = true;
            }

            if (directionVisBoxEditMode)
            {
                if (addChargeMode && 
                    (isMouseInRectanlge(toolLayoutRecs[6], mousePos) ||
                     isMouseInRectanlge(toolLayoutRecs[7], mousePos)))
                {
                    result = true;
                }

                if (editChargeMode &&
                    isMouseInRectanlge(toolLayoutRecs[5], mousePos))
                {
                   result = true;
                }

                if (eraseMode &&
                    isMouseInRectanlge(toolLayoutRecs[4], mousePos))
                {
                    result = true;
                }

                if (toolLayoutRecs[0..4].Any(a => isMouseInRectanlge(a, mousePos) == true) ||
                    isMouseInRectanlge(toolLayoutRecs[8], mousePos))
                {
                    result = true;
                }
            }

            if (showSettings)
            {
                if (settingsLayoutRecs.Any(a => isMouseInRectanlge(a, mousePos) == true))
                {
                    result = true;
                }
            }

            return result;
        }

        bool isMouseInRectanlge(Rectangle rect, Vector2 mousePos)
        {
            return rect.x < mousePos.X && 
                   rect.x + rect.width > mousePos.X &&
                   rect.y < mousePos.Y && 
                   rect.y + rect.height > mousePos.Y;
        }
        /*
         * Global - 
         *  new Rectangle( 80, 24, 48, 48),    // Button: settingsButton
            new Rectangle( 24, 24, 48, 48 ),    // Button: toolButton

           Tools only -
            new Rectangle( 24, 96, 48, 24 ),    // Button: addButton
            new Rectangle( 24, 128, 48, 24 ),    // Button: removeButton
            new Rectangle( 24, 192, 48, 24 ),    // Button: dragMoveButton
            new Rectangle( 24, 160, 48, 24 ),    // Button: editChargeButton
            new Rectangle( 80, 128, 48, 24 ),    // Button: RemoveButtonClear
            new Rectangle( 80, 160, 48, 24 ),    // Button: editChargeButtonSetZero
            new Rectangle( 80, 96, 24, 24 ),    // Button: addButtonPositive
            new Rectangle( 104, 96, 24, 24 ),    // Button: addButtonNegative
           
           Settings only -
            new Rectangle( 136, 24, 272, 328 ),    // WindowBox: settingsWindowBox
            new Rectangle( 144, 64, 256, 120 ),    // GroupBox: GroupBox012
            new Rectangle( 272, 80, 112, 16 ),    // Label: Label015
            new Rectangle( 272, 104, 120, 16 ),    // Label: Label016
            new Rectangle( 152, 152, 120, 16 ),    // Spinner: probesPerChargeSpinner
            new Rectangle( 272, 152, 120, 16 ),    // Label: Label018
            new Rectangle( 272, 128, 120, 16 ),    // Label: Label020
            new Rectangle( 144, 200, 256, 144 ),    // GroupBox: GroupBox021
            new Rectangle( 152, 216, 112, 16 ),    // Spinner: lineThicknessSpinner
            new Rectangle( 264, 216, 120, 16 ),    // Label: Label023
            new Rectangle( 152, 80, 120, 16 ),    // Spinner: qualitySpinner
            new Rectangle( 152, 104, 120, 16 ),    // Spinner: lodQualitySpinner
            new Rectangle( 152, 128, 120, 16 ),    // Spinner: probeRadiusSpinner
            new Rectangle( 152, 240, 112, 24 ),    // DropdownBox: directionVisBox
            new Rectangle( 264, 240, 120, 24 ),    // Label: Label027
            new Rectangle( 152, 272, 112, 24 ),    // Button: showLinesButton
            new Rectangle( 152, 304, 112, 24 ),    // Button: showDotsButton
         * 
         * 
         */
        public void DrawPollGui()
        {

            // raygui: controls drawing
            //----------------------------------------------------------------------------------
            // Draw controls

            if (directionVisBoxEditMode) RayGui.GuiLock();

            if (RayGui.GuiButton(layoutRecs[0], "#142#")) SettingsButton();
            if (RayGui.GuiButton(layoutRecs[1], "#140#")) ToolButton();

            if (showTools)
            {
                if (RayGui.GuiButton(layoutRecs[2], "#022#")) AddButton();
                if (addChargeMode)
                {
                    if (RayGui.GuiButton(layoutRecs[27], "+")) AddButtonPositive();
                    if (RayGui.GuiButton(layoutRecs[28], "-")) AddButtonNegative();
                }
                if (RayGui.GuiButton(layoutRecs[3], "#028#")) RemoveButton();
                if (eraseMode) if (RayGui.GuiButton(layoutRecs[6], "Clear")) RemoveButtonClear();
                if (RayGui.GuiButton(layoutRecs[4], "#021#")) DragMoveButton();
                if (RayGui.GuiButton(layoutRecs[5], "#041#")) EditChargeButton();
                if (editChargeMode) if (RayGui.GuiButton(layoutRecs[7], "Set 0")) EditChargeButtonSetZero();
                if (RayGui.GuiButton(layoutRecs[9], "#043#")) ZoomButton();
            }

            if (settingsWindowBoxActive)
            {
                settingsWindowBoxActive = !RayGui.GuiWindowBox(layoutRecs[8], "Settings");
                RayGui.GuiGroupBox(layoutRecs[10], "Simulation");
                RayGui.GuiLabel(layoutRecs[11], " Quality");
                RayGui.GuiLabel(layoutRecs[12], " LoD Quality");
                fixed (int* probesPerChargePtr = &probesPerChargeSpinnerValue)
                {
                    if (RayGui.GuiSpinner(layoutRecs[13], "", probesPerChargePtr, 0, 100, probesPerChargeSpinnerEditMode)) probesPerChargeSpinnerEditMode = !probesPerChargeSpinnerEditMode;
                }
                RayGui.GuiLabel(layoutRecs[14], " Probes per charge");
                RayGui.GuiLabel(layoutRecs[15], " Probe radius");
                RayGui.GuiGroupBox(layoutRecs[16], "Visualization");
                fixed (int* lineThicknessPtr = &lineThicknessSpinnerValue)
                {
                    if (RayGui.GuiSpinner(layoutRecs[17], "", lineThicknessPtr, 0, 100, lineThicknessSpinnerEditMode)) lineThicknessSpinnerEditMode = !lineThicknessSpinnerEditMode;
                }
                RayGui.GuiLabel(layoutRecs[18], " Field line thickness");
                fixed (int* qualityPtr = &qualitySpinnerValue)
                {
                    if (RayGui.GuiSpinner(layoutRecs[19], "", qualityPtr, 0, 100, qualitySpinnerEditMode)) qualitySpinnerEditMode = !qualitySpinnerEditMode;
                }
                fixed (int* lodQualityPtr = &lodQualitySpinnerValue)
                {
                    if (RayGui.GuiSpinner(layoutRecs[20], "", lodQualityPtr, 0, 100, lodQualitySpinnerEditMode)) lodQualitySpinnerEditMode = !lodQualitySpinnerEditMode;
                }
                fixed (int* probeRadiusPtr = &probeRadiusSpinnerValue)
                {
                    if (RayGui.GuiSpinner(layoutRecs[21], "", probeRadiusPtr, 0, 100, probeRadiusSpinnerEditMode)) probeRadiusSpinnerEditMode = !probeRadiusSpinnerEditMode;
                }
                RayGui.GuiLabel(layoutRecs[23], " Show field direction");
                if (RayGui.GuiButton(layoutRecs[25], "Show Field Lines")) ShowLinesButton();
                if (RayGui.GuiButton(layoutRecs[26], "Show dots")) ShowDotsButton();
                fixed (int* directionVisBoxActivePtr = &directionVisBoxActive)
                {
                    if (RayGui.GuiDropdownBox(layoutRecs[22], "Static;Animated;None", directionVisBoxActivePtr, directionVisBoxEditMode)) directionVisBoxEditMode = !directionVisBoxEditMode;
                }
            }

            RayGui.GuiUnlock();
        }

        //------------------------------------------------------------------------------------
        // Controls Functions Definitions (local)
        //------------------------------------------------------------------------------------
        // Button: settingsButton logic
        void SettingsButton()
        {
            // TODO: Implement control logic
            showSettings = !showSettings;
            settingsWindowBoxActive = true;
        }
        // Button: toolButton logic
        void ToolButton()
        {
            showTools = !showTools;
        }
        // Button: addButton logic
        void AddButton()
        {
            addChargeMode = true;
            zoomMode = false;
            eraseMode = false;
            dragMoveMode = false;
            editChargeMode = false;
        }
        // Button: removeButton logic
        void RemoveButton()
        {
            eraseMode = true;
            dragMoveMode = false;
            addChargeMode = false;
            editChargeMode = false;
            zoomMode = false;
        }
        // Button: dragMoveButton logic
        void DragMoveButton()
        {
            dragMoveMode = true;
            addChargeMode = false;
            eraseMode = false;
            editChargeMode = false;
            zoomMode = false;
        }
        // Button: editChargeButton logic
        void EditChargeButton()
        {
            editChargeMode = true;
            addChargeMode = false;
            eraseMode = false;
            dragMoveMode = false;
            zoomMode = false;
            setChargeToZero = false;
        }
        // Button: RemoveButtonClear logic
        void RemoveButtonClear()
        {
            clearCharges = true;
        }
        // Button: editChargeButtonSetZero logic
        void EditChargeButtonSetZero()
        {
            setChargeToZero = true;
        }
        // Button: zoomButton logic
        void ZoomButton()
        {
            zoomMode = true;
            editChargeMode = false;
            addChargeMode = false;
            eraseMode = false;
            dragMoveMode = false;
        }

        // Button: showLinesButton logic
        void ShowLinesButton()
        {
            showLines = !showLines;
        }
        // Button: showDotsButton logic
        void ShowDotsButton()
        {
            showDots = !showDots;
        }
        // Button: addButtonPositive logic
        void AddButtonPositive()
        {
            chargePolarity = true;
        }
        // Button: addButtonNegative logic
        void AddButtonNegative()
        {
            chargePolarity = false;
        }
    }
}
