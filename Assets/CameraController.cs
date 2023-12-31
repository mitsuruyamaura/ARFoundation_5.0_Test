using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player")]
    public GameObject targetObj;
    private Vector3 targePos;                   //targetObj(Player)変数のゲームオブジェクトの位置を記録する変数
    [SerializeField]
    private float cameraRotateSpeed = 200f;     //カメラの回転速度
    
    [SerializeField]
    private float cameraRotateSpeedY = 0f;     //カメラの回転速度
    
    
    [SerializeField]
    private float maxLimit = 30.0f;             //X軸方向の可動範囲
    private float minLimit;
    public bool isGamePad;
    private void Start()
    {
        //targePosにPlayerの位置情報(transform.position)を代入する
        targePos = targetObj.transform.position;
        //X軸の可動範囲の設定
        minLimit = 360 - maxLimit;
    }
    private void Update()
    {
        //追従対象がいる場合
        if (targetObj!= null)
        {
            //カメラの位置を、追従対象の位置 - 補正値(targetPos)にして、一定距離離れて追従させる
            transform.position += targetObj.transform.position - targePos;
            //追従対象(targetObj)の位置情報を更新
            targePos = targetObj.transform.position;
            RotateCamer();
        }
        if (Input.GetMouseButton(1))
        {
            //カメラの回転
            //RotateCamer();
        }
    }
    private void RotateCamer()
    {
        //マウスの入力値を取得
        float x = Input.GetAxis("Mouse X");
        float z = -Input.GetAxis("Mouse Y");
        
        if (isGamePad == true)
        {
            x = Input.GetAxis("Horizontal2");
            z = Input.GetAxis("Vertical2");
        }
        
        //カメラの追従対象の周囲を公転回転させる
        transform.RotateAround(targetObj.transform.position, Vector3.up, x * Time.deltaTime * cameraRotateSpeed);
        transform.RotateAround(targetObj.transform.position, transform.right, z * Time.deltaTime * cameraRotateSpeedY);
        
        //カメラの回転情報の初期値をセット
        var localAngle = transform.localEulerAngles;
        
        // X軸の回転情報をセット
        localAngle.x += z;
        // X軸を可動範囲に収まるように制御
        if (localAngle.x > maxLimit && localAngle.x < 180)
        {
            localAngle.x = maxLimit;
        }
        if (localAngle.x <minLimit && localAngle.x > 180)
        {
            localAngle.x = minLimit;
        }
        //カメラの回転
        transform.localEulerAngles = localAngle;
    }
}
