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


        // Define controls rectangles
        Rectangle[] layoutRecs = new Rectangle[]{
            new Rectangle ( 8, 8, 48, 48 ),    // 0  Button: toolButton
            new Rectangle ( 8, 64, 48, 24 ),    // 1  Button: addButton
            new Rectangle ( 8, 96, 48, 24 ),    // 2  Button: removeButton
            new Rectangle ( 64, 8, 48, 48 ),    // 3  Button: settingsButton
            new Rectangle ( 8, 128, 48, 24 ),    // 4  Button: editChargeButton
            new Rectangle ( 8, 160, 48, 24 ),    // 5  Button: dragMoveButton
            new Rectangle ( 8, 192, 48, 24 ),    // 6  Button: zoomButton
            new Rectangle ( 64, 64, 24, 24 ),    // 7  Button: addButtonPositive
            new Rectangle ( 88, 64, 24, 24 ),    // 8  Button: addButtonNegative
            new Rectangle ( 64, 96, 48, 24 ),    // 9  Button: Button009 ~
            new Rectangle ( 64, 128, 48, 24 ),    // 10 Button: editChargeButtonSetZero

            new Rectangle ( 120, 8, 280, 296 ),    // 11 WindowBox: settingsWindowBox ~
            new Rectangle ( 128, 40, 264, 120 ),    // 12 GroupBox: GroupBox012 ~
            new Rectangle ( 136, 56, 104, 16 ),    // 13 Spinner: QualitySpinner  ~
            new Rectangle ( 136, 80, 104, 16 ),    // 14 Spinner: lodQualitySpinner
            new Rectangle ( 136, 104, 104, 16 ),    // 15 Spinner: proveRadiusSpinner ~
            new Rectangle ( 136, 128, 104, 16 ),    // 16 Spinner: probesPerChargeSpinner ~
            new Rectangle ( 240, 56, 96, 16 ),    // 17 Label: Label017 ~
            new Rectangle ( 240, 80, 104, 12 ),    // 18 Label: Label018 ~
            new Rectangle ( 240, 104, 112, 16 ),    // 19 Label:  Probe radius
            new Rectangle ( 240, 128, 112, 16 ),    // 20 Label: Label020
            new Rectangle ( 128, 168, 264, 128 ),    // 21 GroupBox: GroupBox021 ~
            new Rectangle ( 136, 184, 104, 16 ),    // 22 Spinner: uiScaleSpinner ~
            new Rectangle ( 136, 208, 104, 16 ),    // 23 Spinner: lineThicknessSpinner ~
            new Rectangle ( 136, 232, 104, 24 ),    // 24 DropdownBox: directionVisBox
            new Rectangle ( 136, 264, 104, 24 ),    // 25 Button: showLinesButton ~
            new Rectangle ( 248, 264, 104, 24 ),    // 26 Button: showDotsButton ~
            new Rectangle ( 240, 184, 112, 16 ),    // 27 Label: Label028 ~
            new Rectangle ( 240, 208, 112, 16 ),    // 28 Label: Label029 ~
            new Rectangle ( 240, 232, 152, 24 ),    // 29 Label: Label030 ~ 
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
        // Define controls rectangles
        Rectangle[] ogLayoutRecs = new Rectangle[]{
            new Rectangle ( 8, 8, 48, 48 ),    // 0  Button: toolButton
            new Rectangle ( 8, 64, 48, 24 ),    // 1  Button: addButton
            new Rectangle ( 8, 96, 48, 24 ),    // 2  Button: removeButton
            new Rectangle ( 64, 8, 48, 48 ),    // 3  Button: settingsButton
            new Rectangle ( 8, 128, 48, 24 ),    // 4  Button: editChargeButton
            new Rectangle ( 8, 160, 48, 24 ),    // 5  Button: dragMoveButton
            new Rectangle ( 8, 192, 48, 24 ),    // 6  Button: zoomButton
            new Rectangle ( 64, 64, 24, 24 ),    // 7  Button: addButtonPositive
            new Rectangle ( 88, 64, 24, 24 ),    // 8  Button: addButtonNegative
            new Rectangle ( 64, 96, 48, 24 ),    // 9  Button: Button009
            new Rectangle ( 64, 128, 48, 24 ),    // 10 Button: editChargeButtonSetZero
            new Rectangle ( 120, 8, 280, 296 ),    // 11 WindowBox: settingsWindowBox
            new Rectangle ( 128, 40, 264, 120 ),    // 12 GroupBox: GroupBox012
            new Rectangle ( 136, 56, 104, 16 ),    // 13 Spinner: QualitySpinner
            new Rectangle ( 136, 80, 104, 16 ),    // 14 Spinner: Spinner014
            new Rectangle ( 136, 104, 104, 16 ),    // 15 Spinner: proveRadiusSpinner
            new Rectangle ( 136, 128, 104, 16 ),    // 16 Spinner: probesPerChargeSpinner
            new Rectangle ( 240, 56, 96, 16 ),    // 17 Label: Label017
            new Rectangle ( 240, 80, 104, 12 ),    // 18 Label: Label018
            new Rectangle ( 240, 104, 112, 16 ),    // 19 Label:  Probe radius ~
            new Rectangle ( 240, 128, 112, 16 ),    // 20 Label: Label020 ~
            new Rectangle ( 128, 168, 264, 128 ),    // 21 GroupBox: GroupBox021
            new Rectangle ( 136, 184, 104, 16 ),    // 22 Spinner: uiScaleSpinner
            new Rectangle ( 136, 208, 104, 16 ),    // 23 Spinner: lineThicknessSpinner `
            new Rectangle ( 136, 232, 104, 24 ),    // 24 DropdownBox: directionVisBox
            new Rectangle ( 136, 264, 104, 24 ),    // 25 Button: showLinesButton
            new Rectangle ( 248, 264, 104, 24 ),    // 26 Button: showDotsButton
            new Rectangle ( 240, 184, 112, 16 ),    // 27 Label: Label028
            new Rectangle ( 240, 208, 112, 16 ),    // 28 Label: Label029
            new Rectangle ( 240, 232, 152, 24 ),    // 29 Label: Label030
        };

        Rectangle[] ogMainLayoutRecs = new Rectangle[]
        {
            new Rectangle( 80, 24, 48, 48),    // Button: settingsButton
            new Rectangle( 24, 24, 48, 48 ),    // Button: toolButton
        };

        Rectangle[] ogToolLayoutRecs = new Rectangle[]
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

        Rectangle[] ogsettingsLayoutRecs = new Rectangle[]
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
        
        //--------------------------------------------------------------------------------------

        // to be depricated and removed
        private Rectangle scaleRectangle(Rectangle ogRect, float scaleFactor)
        {
                Rectangle scaled = new Rectangle(
                    ogRect.x * scaleFactor,
                    ogRect.Y * scaleFactor,
                    ogRect.width * scaleFactor,
                    ogRect.height * scaleFactor
                );

                return scaled;
            
        }

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

        // to be depricated and removed
        public void scaleUi(float scaleFactor)
        {
            //Console.WriteLine("scaleui called!");
            
            for (int i = 0; i < ogLayoutRecs.Length; i++)
            {
                layoutRecs[i] = scaleRectangle(ogLayoutRecs[i], scaleFactor);
            }
            for (int i = 0; i < ogMainLayoutRecs.Length; i++)
            {
                mainLayoutRecs[i] = scaleRectangle(ogMainLayoutRecs[i], scaleFactor);
            }
            for (int i = 0; i < ogToolLayoutRecs.Length; i++)
            {
                toolLayoutRecs[i] = scaleRectangle(ogToolLayoutRecs[i], scaleFactor);
            }
            for (int i = 0; i < ogsettingsLayoutRecs.Length; i++)
            {
                settingsLayoutRecs[i] = scaleRectangle(ogsettingsLayoutRecs[i], scaleFactor);
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

        // to be depricated and removed.
        //     i wrote this with the worst headache i've ever had
        //     TODO: Refactor
        public bool isMouseOnControls(Vector2 mousePos)
        {
            bool result = false;

            if (mainLayoutRecs.Any(a => isMouseInRectanlge(a, mousePos) == true))
            {
                Console.WriteLine("any");
                result = true;
            }

            
            if (showTools)
            {
                Console.WriteLine("tools");
                if (toolLayoutRecs.Any(a => isMouseInRectanlge(a, mousePos) == true))
                {
                    result = true;
                }
            }

            if (showSettings)
            {
                Console.WriteLine("settings");
                if (settingsLayoutRecs.Any(a => isMouseInRectanlge(a, mousePos) == true))
                {
                    result = true;
                }
            }
            return result;
        }

        // to be depricated and removed
        bool isMouseInRectanlge(Rectangle rect, Vector2 mousePos)
        {
            return rect.x < mousePos.X && 
                   rect.x + rect.width > mousePos.X &&
                   rect.y < mousePos.Y && 
                   rect.y + rect.height > mousePos.Y;
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
