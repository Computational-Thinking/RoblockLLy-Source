using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaximizeButtonBehavior : MonoBehaviour
{
    private GameObject challengeViewer;
    private GameObject mainCanvas;

    // Variables used for the rescale of the challengeViewer.
    private RectTransform canvasRect;
    private RectTransform challengeViewerRect;
    private float canvasWidth;
    private float canvasHeight;
    private float firstChallengeWidth;
    private float firstChallengeHeight;
    private Vector3 initialChallengeViewerPos; // Initial ChallengeViewer position.
    private Vector3 mainCanvasPosition;
    private GameObject maximizeButton;
    private RectTransform maximizeButtonRect;
    private Vector3 maximizeButtonInitPos; // Initial MaximazeButton position.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //challengeViewer = GameObject.Find("Canvas/ChallengeViewer");
        //smallChallengeViewer = GameObject.Find
        //maximizeButtonSmall = GameObject.Find("Canvas/SmallChallengeViewer/MaximizeButtonSmall");
        //maximizeButtonBig = GameObject.Find("Canvas/SmallChallengeViewer/MaximizeButtonBig");
        //maximizeButton = GameObject.Find("Canvas/MaximizeButton");
        mainCanvas = GameObject.Find("Canvas");
        changeImageScaleSetUp();
    }

    public void changeImageScaleSetUp()
    {
        canvasRect = mainCanvas.GetComponent<RectTransform>();
        challengeViewerRect = challengeViewer.GetComponent<RectTransform>();
        maximizeButtonRect = maximizeButton.GetComponent<RectTransform>();
        canvasWidth = canvasRect.rect.width;
        canvasHeight = canvasRect.rect.height;
        //canvasWidth = 1280;
        //canvasHeight = 688.8232f;
        firstChallengeWidth = challengeViewerRect.rect.width;
        firstChallengeHeight = challengeViewerRect.rect.height;
        initialChallengeViewerPos = challengeViewer.transform.position;
        mainCanvasPosition = mainCanvas.transform.position;
        maximizeButtonInitPos = maximizeButton.transform.position;
    }

    public void changeImageScale()
    {
        float challengeWidth = challengeViewerRect.rect.width;
        float challengeHeight = challengeViewerRect.rect.height;
        if((challengeWidth != canvasWidth) && (challengeHeight != canvasHeight))
        { // If the challenge viewer is small
            challengeViewerRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, canvasHeight);
            challengeViewerRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, canvasWidth);
            challengeViewer.transform.position = mainCanvasPosition;
            //challengeViewerRect.localPosition = canvasRect.localPosition;
            float buttonOffset = (maximizeButtonRect.rect.width / 2);
            float leftTopX = (-canvasWidth/2);
            float leftTopY = (canvasHeight/2);
            // Set the button new position.
            //maximizeButtonRect.localPosition = new Vector3(leftTopX + buttonOffset, leftTopY - buttonOffset, canvasRect.position.z);
        } else { // If the challengeViewer is big
            challengeViewerRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, firstChallengeHeight);
            challengeViewerRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, firstChallengeWidth);
            challengeViewer.transform.position = initialChallengeViewerPos;
            // Sets the button to its initial position.
            maximizeButton.transform.position = maximizeButtonInitPos;
        }
    }

    public void ChangeToSmall()
    {
        
    }
}
