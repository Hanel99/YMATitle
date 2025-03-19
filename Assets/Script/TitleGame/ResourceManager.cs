using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public List<Sprite> ImageResources = new List<Sprite>();
    public List<AudioClip> AudioResources = new List<AudioClip>();
    public List<Sprite> MMGImageResources = new List<Sprite>();
    public List<Sprite> HanelImageResources = new List<Sprite>();


    public Sprite GetImage(QType type)
    {
        int index = (int)type;
        if (index >= 0 && index < ImageResources.Count)
        {
            return ImageResources[index];
        }
        return null;
    }

    public Sprite GetMMGImage(int index)
    {
        if (index >= 0 && index < MMGImageResources.Count)
        {
            return MMGImageResources[index];
        }
        return null;
    }

    public Sprite GetHanelImage(int index)
    {
        if (index >= 0 && index < HanelImageResources.Count)
        {
            return HanelImageResources[index];
        }
        return null;
    }


    public AudioClip GetAudio(SoundType type)
    {
        int index = (int)type;
        if (index >= 0 && index < AudioResources.Count)
        {
            return AudioResources[index];
        }
        return null;
    }


}
