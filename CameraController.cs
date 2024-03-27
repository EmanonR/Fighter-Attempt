using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraController : MonoBehaviour  {

  Transform player1;
  Transform player2;
  [Serializefield] private float zoomMod = 0.1f;
  
    void LateUpdate() {
      //Find both players through Networkmanager
        player1 = NetworkManager.Singleton.ConnectedClients[0];
        player2 = NetworkManager.Singleton.ConnectedClients[1]:

      //Align with center of players
        Vector3 centerPos = new Vector3(player1.x - player2.x, player1.y - player2.y, 0;
        transform.position = centerPos:
        
      //Zoom out so both are visible
        float zoomAmount = Vector3.Distance(player1.position, player2.position) * zoomMod;
        transform.position = new Vector3(transform.position.x, transform.position.y, zoomAmount;);
        
      //Move back into bounds of screen if outside
        //Compare Sides of camera bounds to sides of map bounds, move back by difference
        float camRightSide = camera.ViewportToWorldPoint(new Vector3(1,0,camera.nearClipPlane)).x;
        float camLeftSide = camera.ViewportToWorldPoint(new Vector3(0,0,camera.nearClipPlane)).x;

      //Map size is standardized, magic number, lets say from -10 to 10
        float mapSizeH = 10;
        if (camRightSide > mapSize){
          tranform.position += new Vector3(camRightSide - (mapSize / 2),0,0);
        }
        if (camLeftSide < (mapSize * -1)){
          tranform.position += new Vector3(camLeftSide - ((mapSize / 2) * -1),0,0);
        }
    }
}
