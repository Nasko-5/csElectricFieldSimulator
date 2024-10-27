using Raylib_CsLo;
using System.Numerics;

namespace csElectricFieldSimulator
{
    public unsafe class Gui
    {
        // csElectricFieldSimulator: controls initialization
        //----------------------------------------------------------------------------------
        // Define controls variables
        bool settingsWindowBoxActive = false;            // WindowBox: settingsWindowBox
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
        public int directionVisBoxActive = 1;            // DropdownBox: directionVisBox
        bool uiScaleSpinnerEditMode = false;
        int uiScaleSpinnerValue = 0;            // Spinner: uiScaleSpinner

        // states
        public bool showTools = false;
        public bool showSettings = false;
        public bool chargePolarity = false; // false = negative ; true = postive
        public bool dragMoveMode = true;
        public bool addChargeMode = false;
        public bool eraseMode = false;
        public bool editChargeMode = false;
        public bool zoomMode = false;

        // options
        public bool setChargeToZero = false;
        public bool clearCharges = false;
        public bool showLines = true;
        public bool showDots = false;

        Rectangle anchor = new Rectangle(80, 24, 48, 48);

        public Dictionary<string, uiElement> newrecs = new Dictionary<string, uiElement>()
        {  // 
           // 
           // .________________.                               .____________________.  ._________.
           // |      NAME      |   ~ tool & setting button ~   |    CONTROL RECT    |  |  CTRLG  | (control group)
           // |````````````````|                               |````````````````````|  |`````````|
            {       "toolButton", new uiElement( new Rectangle ( 8  , 8  , 48 , 48  ) ,     1    ) },
            {   "settingsButton", new uiElement( new Rectangle ( 64 , 8  , 48 , 48  ) ,     1    ) },
           // |________________|                               |____________________|  |_________|
          
           // .________________.                               .____________________.  ._________.
           // |      NAME      |            ~ tools ~          |    CONTROL RECT    |  |  CTRLG  | (control group)
           // |````````````````|                               |````````````````````|  |`````````|
            {        "addButton", new uiElement( new Rectangle ( 8  , 64 , 48 , 24  ) ,     2    ) },
            {     "removeButton", new uiElement( new Rectangle ( 8  , 96 , 48 , 24  ) ,     2    ) },
            { "editChargeButton", new uiElement( new Rectangle ( 8  , 128, 48 , 24  ) ,     2    ) },
            {   "dragMoveButton", new uiElement( new Rectangle ( 8  , 160, 48 , 24  ) ,     2    ) },
            {       "zoomButton", new uiElement( new Rectangle ( 8  , 192, 48 , 24  ) ,     2    ) }, 
           // |________________|                               |____________________|  |_________|

           // .________________.                               .____________________.  ._________.
           // |      NAME      |       ~ tool specific ~       |    CONTROL RECT    |  |  CTRLG  | (control group)
           // |````````````````|                               |````````````````````|  |`````````|
            {     "addButtonPos", new uiElement( new Rectangle ( 64 , 64 , 24 , 24  ) ,     3    ) },
            {     "addButtonNeg", new uiElement( new Rectangle ( 88 , 64 , 24 , 24  ) ,     3    ) },
            {   "editChargeSet0", new uiElement( new Rectangle ( 64 , 128, 48 , 24  ) ,     4    ) },
            {      "removeClear", new uiElement( new Rectangle ( 64 , 96 , 48 , 24  ) ,     5    ) }, 
           // |________________|                               |____________________|  |_________|

           // .________________.                               .____________________.  ._________.
           // |      NAME      |         ~ settings ~          |    CONTROL RECT    |  |  CTRLG  | (control group)
           // |````````````````|                               |````````````````````|  |`````````|
            { "settingWindowBox", new uiElement( new Rectangle ( 120, 8  , 280, 296 ) ,     6    ) },
            {      "simGroupBox", new uiElement( new Rectangle ( 128, 40 , 264, 120 ) ,     6    ) },
            {      "visGroupBox", new uiElement( new Rectangle ( 128, 168, 264, 128 ) ,     6    ) },
           // +----------------+                               +--------------------+  +---------+
            {         "qualityL", new uiElement( new Rectangle ( 240, 56 , 96 , 16  ) ,     6    ) }, // <+ labels
            {      "lodQualityL", new uiElement( new Rectangle ( 240, 80 , 104, 12  ) ,     6    ) }, //  |
            {     "probeRadiusL", new uiElement( new Rectangle ( 240, 104, 112, 16  ) ,     6    ) }, //  |
            { "probesPerChargeL", new uiElement( new Rectangle ( 240, 128, 112, 16  ) ,     6    ) }, //  |
            {   "lineThicknessL", new uiElement( new Rectangle ( 240, 208, 112, 16  ) ,     6    ) }, //  |
            {         "uiScaleL", new uiElement( new Rectangle ( 240, 184, 112, 16  ) ,     6    ) }, //  |
            {  "fieldDirectionL", new uiElement( new Rectangle ( 240, 232, 152, 24  ) ,     6    ) }, // <+
           // +----------------+                               +--------------------+  +---------+
            {         "qualityS", new uiElement( new Rectangle ( 136, 56 , 104, 16  ) ,     6    ) }, // <+ spinners
            {      "lodQualityS", new uiElement( new Rectangle ( 136, 80 , 104, 16  ) ,     6    ) }, //  |
            {     "probeRadiusS", new uiElement( new Rectangle ( 136, 104, 104, 16  ) ,     6    ) }, //  |
            { "probesPerChargeS", new uiElement( new Rectangle ( 136, 128, 104, 16  ) ,     6    ) }, //  |
            {   "lineThicknessS", new uiElement( new Rectangle ( 136, 208, 104, 16  ) ,     6    ) }, //  |
            {         "uiScaleS", new uiElement( new Rectangle ( 136, 184, 104, 16  ) ,     6    ) }, // <+
           // +----------------+                               +--------------------+  +---------+
            {  "showLinesButton", new uiElement( new Rectangle ( 136, 264, 104, 24  ) ,     6    ) }, // button
            {   "showDotsButton", new uiElement( new Rectangle ( 248, 264, 104, 24  ) ,     6    ) }, // button
            {  "directionVisBox", new uiElement( new Rectangle ( 136, 232, 104, 24  ) ,     6    ) }, // dropdown
            {        "Button009", new uiElement( new Rectangle ( 64 , 96 , 48 , 24  ) ,     6    ) }, // ? unused
           // |________________|                               |____________________|  |_________|
        };

        public void newScale(float scaleFactor) { 
            foreach (KeyValuePair<string, uiElement> entry in newrecs)
            {
                entry.Value.scaleRect(scaleFactor);
            }

            RayGui.GuiSetStyle(
                (int)Raylib_CsLo.GuiControl.DEFAULT,
                (int)Raylib_CsLo.GuiDefaultProperty.TEXT_SIZE,
                (int)(10 * scaleFactor)
            );
        }

        public (int probesPerCharge, int probeRadius, int lodQuality, int quality, int uiScale) getSettings()
        {
            return (
                probesPerCharge: probesPerChargeSpinnerValue,
                probeRadius: probeRadiusSpinnerValue,
                lodQuality: lodQualitySpinnerValue,
                quality: qualitySpinnerValue,
                uiScale: uiScaleSpinnerValue
            );
        }

        public bool newIsMouseOnControls(Vector2 mousepos)
        {
            // If the mouse is on any of the uiElement/s, and has a ControlGroup of 1, return true
            if (newrecs.Any(a => a.Value.isMouseInRectanlge(mousepos) && a.Value.ControlGroup == 1))
            {
                return true;
            }

            if (showTools && newrecs.Any(a => a.Value.isMouseInRectanlge(mousepos) && 
               (a.Value.ControlGroup == 2 ||
                a.Value.ControlGroup == 3 && addChargeMode ||
                a.Value.ControlGroup == 4 && editChargeMode ||
                a.Value.ControlGroup == 5 && eraseMode))
            )
            {
                return true;
            }

            if (showSettings && newrecs.Any(a => a.Value.isMouseInRectanlge(mousepos) && a.Value.ControlGroup == 6))
            {
                return true;
            }

            return false;
        }

        public void DrawPollGui()
        {

            // raygui: controls drawing
            //----------------------------------------------------------------------------------
            // Draw controls

            if (directionVisBoxEditMode) RayGui.GuiLock();

            if (RayGui.GuiButton(newrecs["settingsButton"].Scaled, "#142#")) SettingsButton();
            if (RayGui.GuiButton(newrecs["toolButton"].Scaled, "#140#")) ToolButton();

            if (showTools)
            {
                if (RayGui.GuiButton(newrecs["addButton"].Scaled, "#022#")) AddButton();
                if (addChargeMode)
                {
                    if (RayGui.GuiButton(newrecs["addButtonPos"].Scaled, "+")) AddButtonPositive();
                    if (RayGui.GuiButton(newrecs["addButtonNeg"].Scaled, "-")) AddButtonNegative();
                }
                if (RayGui.GuiButton(newrecs["removeButton"].Scaled, "#028#")) RemoveButton();
                if (eraseMode) if (RayGui.GuiButton(newrecs["removeClear"].Scaled, "Clear")) RemoveButtonClear();
                if (RayGui.GuiButton(newrecs["dragMoveButton"].Scaled, "#021#")) DragMoveButton();
                if (RayGui.GuiButton(newrecs["editChargeButton"].Scaled, "#041#")) EditChargeButton();
                if (editChargeMode) if (RayGui.GuiButton(newrecs["editChargeSet0"].Scaled, "Set 0")) EditChargeButtonSetZero();
                if (RayGui.GuiButton(newrecs["zoomButton"].Scaled, "#043#")) ZoomButton();
            }

            if (showSettings)
            {
                
                showSettings = !RayGui.GuiWindowBox(newrecs["settingWindowBox"].Scaled, "#142#Settings");
                RayGui.GuiGroupBox(newrecs["simGroupBox"].Scaled, "Simulation");
                RayGui.GuiLabel(newrecs["qualityL"].Scaled, " Quality");
                RayGui.GuiLabel(newrecs["lodQualityL"].Scaled, " LoD Quality");
                fixed (int* probesPerChargePtr = &probesPerChargeSpinnerValue)
                {
                    if (RayGui.GuiSpinner(newrecs["probesPerChargeS"].Scaled, "", probesPerChargePtr, 0, 100, probesPerChargeSpinnerEditMode)) probesPerChargeSpinnerEditMode = !probesPerChargeSpinnerEditMode;
                }
                RayGui.GuiLabel(newrecs["probesPerChargeL"].Scaled, " Probes per charge");
                RayGui.GuiLabel(newrecs["probeRadiusL"].Scaled, " Probe radius");
                RayGui.GuiGroupBox(newrecs["visGroupBox"].Scaled, "Visualization");
                fixed (int* lineThicknessPtr = &lineThicknessSpinnerValue)
                {
                    if (RayGui.GuiSpinner(newrecs["lineThicknessS"].Scaled, "", lineThicknessPtr, 0, 100, lineThicknessSpinnerEditMode)) lineThicknessSpinnerEditMode = !lineThicknessSpinnerEditMode;
                }
                RayGui.GuiLabel(newrecs["lineThicknessL"].Scaled, " Field line thickness");
                fixed (int* qualityPtr = &qualitySpinnerValue)
                {
                    if (RayGui.GuiSpinner(newrecs["qualityS"].Scaled, "", qualityPtr, 0, 100, qualitySpinnerEditMode)) qualitySpinnerEditMode = !qualitySpinnerEditMode;
                }
                fixed (int* lodQualityPtr = &lodQualitySpinnerValue)
                {
                    if (RayGui.GuiSpinner(newrecs["lodQualityS"].Scaled, "", lodQualityPtr, 0, 100, lodQualitySpinnerEditMode)) lodQualitySpinnerEditMode = !lodQualitySpinnerEditMode;
                }
                fixed (int* probeRadiusPtr = &probeRadiusSpinnerValue)
                {
                    if (RayGui.GuiSpinner(newrecs["probeRadiusS"].Scaled, "", probeRadiusPtr, 0, 100, probeRadiusSpinnerEditMode)) probeRadiusSpinnerEditMode = !probeRadiusSpinnerEditMode;
                }
                RayGui.GuiLabel(newrecs["fieldDirectionL"].Scaled, " Show field direction");
                RayGui.GuiLabel(newrecs["uiScaleL"].Scaled, " UI scale");
                fixed (int* uiScalePtr = &uiScaleSpinnerValue)
                {
                    if (RayGui.GuiSpinner(newrecs["uiScaleS"].Scaled, "", uiScalePtr, 0, 100, uiScaleSpinnerEditMode)) uiScaleSpinnerEditMode = !uiScaleSpinnerEditMode;
                }
                if (RayGui.GuiButton(newrecs["showLinesButton"].Scaled, "Show Field Lines")) ShowLinesButton();
                if (RayGui.GuiButton(newrecs["showDotsButton"].Scaled, "Show dots")) ShowDotsButton();
                fixed (int* directionVisBoxActivePtr = &directionVisBoxActive)
                {
                    if (RayGui.GuiDropdownBox(newrecs["directionVisBox"].Scaled, "Static;Animated;None", directionVisBoxActivePtr, directionVisBoxEditMode)) directionVisBoxEditMode = !directionVisBoxEditMode;
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
            settingsWindowBoxActive = settingsWindowBoxActive;
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
