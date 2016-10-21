using UnityEngine;
using System.Collections;

public class FripperController : MonoBehaviour {

	private HingeJoint myHingeJoynt;	// HingiJointコンポーネント
	private float defaultAngle = 20;	// 初期の傾き
	private float flickAngle = -20;		// 弾いた時の傾き

	// Use this for initialization
	void Start () {
		// HinjiJointコンポーネント取得
		this.myHingeJoynt = GetComponent<HingeJoint>();

		// フリッパーの傾きを設定
		SetAngle(this.defaultAngle);
	}
	
	// Update is called once per frame
	void Update () {
		// 左矢印キーを押した時左フリッパーを動かす
		if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag") {
			SetAngle(this.flickAngle);
		}

		// 右矢印キーを押した時右フリッパーを動かす
		if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag") {
			SetAngle(this.flickAngle);
		}

		// 矢印キー離された時フリッパーを元に戻す
		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
			SetAngle(this.defaultAngle);
		}

		// タッチによるフリッパー制御
		this.ControllFripperByTouch();
	}

	// タッチによるフリッパー制御
	private void ControllFripperByTouch () {
		foreach (Touch touch in Input.touches) {
			// 画面の左半分タップ時は左側のフリッパーを、右半分タップ時は右側のフリッパーを動かす
			if ((tag == "LeftFripperTag" && touch.position.x <= Screen.width/2) || 
				(tag == "RightFripperTag" && touch.position.x > Screen.width/2)) {
				switch(touch.phase) {
					case TouchPhase.Began:
					case TouchPhase.Moved:
					case TouchPhase.Stationary:
						// 上にはじく
						SetAngle(this.flickAngle);
						break;
					case TouchPhase.Ended:
					case TouchPhase.Canceled:
						// 下に戻す
						SetAngle(this.defaultAngle);
						break;
				}
			}
		}
	}

	// フリッパーの傾きを設定
	public void SetAngle (float angle) {
		JointSpring jointSpr = this.myHingeJoynt.spring;
		jointSpr.targetPosition = angle;
		this.myHingeJoynt.spring = jointSpr;
	}
}
