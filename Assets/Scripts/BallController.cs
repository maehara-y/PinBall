using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	// ボールが見える可能性のあるz軸の最大値
	private float visiblePosZ = -6.5f;

	// ゲームオーバーを表示するテキスト
	private GameObject gameOverText;

	// 得点を表示用
	private int point = 0;
	private int pointSmallStar = 10;
	private int pointLargeStar = 50;
	private int pointSmallCloud = 30;
	private int pointLargeCloud = 100;
	private GameObject pointText;

	// Use this for initialization
	void Start () {
		// シーン中のGameOverTextオブジェクトを取得
		this.gameOverText = GameObject.Find("GameOverText");

		// シーン中のGameOverTextオブジェクトを取得
		this.pointText = GameObject.Find("PointText");
	}
	
	// Update is called once per frame
	void Update () {
		// ボールが画面外に出た場合
		if (this.transform.position.z < this.visiblePosZ) {
			// GameoverTextにゲームオーバを表示
			this.gameOverText.GetComponent<Text>().text = "Game Over!!";
		}
	}

	// 衝突時に得点を加算する
	void OnCollisionEnter(Collision other) {
		int addPoint = 0;
		if (other.gameObject.tag == "SmallStarTag") {
			addPoint = this.pointSmallStar;
		} else if (other.gameObject.tag == "LargeStarTag") {
			addPoint = this.pointLargeStar;
		} else if (other.gameObject.tag == "SmallCloudTag") {
			addPoint = this.pointSmallCloud;
		} else if (other.gameObject.tag == "LargeCloudTag") {
			addPoint = this.pointLargeCloud;
		}
		this.point += addPoint;
		this.pointText.GetComponent<Text>().text = "Point : " + this.point.ToString();
	}
}
