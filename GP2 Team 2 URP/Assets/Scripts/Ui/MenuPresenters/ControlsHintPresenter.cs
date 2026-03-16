using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

public class ControlsHintPresenter
{

    private Dictionary<Label,VisualElement> controlPairs;
    //private Button debugEndStateButton;
    //add a way to pull this from editor instead of hard coding

    public ControlsHintPresenter(VisualElement root, UiController controller)
    {
        controlPairs = new Dictionary<Label,VisualElement>();

        VisualElement pair1 = root.Q("Control1");
        VisualElement pair2 = root.Q("Control2");
        VisualElement pair3 = root.Q("Control3");
        VisualElement pair4 = root.Q("Control4");

        //debugEndStateButton = root.Q<Button>("EndStateButton");
        //debugEndStateButton.clicked += () => GameManager.Instance.switchState<EndState>();
        /*
        
        Label control1 = pair1.Q<Label>("ActionName");
        VisualElement control1Icon = pair1.Q<VisualElement>("Image");
        controlPairs.Add(control1, control1Icon);

        Label control2 = pair2.Q<Label>("ActionName");
        VisualElement control2Icon = pair2.Q<VisualElement>("Image");
        controlPairs.Add(control2, control2Icon);

        Label control3 = pair3.Q<Label>("ActionName");
        VisualElement control3Icon = pair3.Q<VisualElement>("Image");
        controlPairs.Add(control3, control3Icon);

        Label control4 = pair4.Q<Label>("ActionName");
        VisualElement control4Icon = pair3.Q<VisualElement>("Image");
        controlPairs.Add(control4, control4Icon);

        SetControlData(controller);
        */
    }

    private void SetControlData(UiController controller)
    {
        List<Label> keys = controlPairs.Keys.ToList();
        for (int i = 0; i <controlPairs.Count; i++) 
        {
            if(controller.ActionNames.Count > i)
            {
                string actionName = controller.ActionNames[i];
                keys[i].text = actionName;
            }
        }
    }
}
