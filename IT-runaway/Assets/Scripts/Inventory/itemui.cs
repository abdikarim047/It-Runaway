using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class itemui : MonoBehaviour
{
  public itemstack stack;
    public RawImage iconDisplay;
    public init(itemstack stack)
    {
      stack = newStack;
      
       updateslot();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject currentItemPreview;
    private Camera previewCamera;
    private RenderTexture renderTexture;

    public void updateslot()
    {
        ClearSlot();
        if (stack == null || stack.item == null) return;

        renderTexture = new RenderTexture(256, 256, 16);
        
        GameObject camObj = new GameObject("previewcamera");
        camObj.transform.position = new UnityEngine.Vector3(0,0, -3);
        previewCamera = camObj.AddComponent<Camera>();
        previewCamera.targetTexture = renderTexture;
        previewCamera.clearFlags = CameraClearFlags.SolidColor;


        currentItemPreview = Instantiate(stack.item.itemprefab);
        currentItemPreview.transform.position = Vector3.Zero;
        currentItemPreview.transform.rotation = quaternion.Euler(25,45,0);
    }

  public void ClearSlot()
  {
    if (currentItemPreview != null) Destroy(currentItemPreview);
    if (previewCamera != null) Destroy(previewCamera.gameObject);
    if (renderTexture != null) renderTexture.Release();
    if (iconDisplay != null) iconDisplay.texture = null;
  }
}
