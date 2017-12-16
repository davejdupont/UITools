using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardLayout : UILayout {

	public RawImage image;
	public Text cardName;
	public Text stats;
	public Text description;

	CardData mData;

	public override void SetData(object o){
		mData = o as CardData;

//		image.texture = new WWW( mData.imageURL).texture;
		cardName.text = mData.cardName;
		stats.text = mData.attack + " / " + mData.defense;
		description.text = mData.description;
	}

}
