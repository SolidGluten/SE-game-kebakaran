using Godot;
using System;

public partial class GuideTrigger : Button
{
	private Control guideModal;

  public override void _Ready()
  {
		guideModal = GetNode<Control>("../GuideModal");
		if(guideModal != null) this.Pressed += EnableGuide;
  }

	public void EnableGuide(){
		guideModal.Visible = !guideModal.Visible;
	}

}
