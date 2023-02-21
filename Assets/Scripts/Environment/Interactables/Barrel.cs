using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public string savePath;
    public bool IsDestroyed;
    [SerializeField]
    private GameObject VfxOnDestroy;

    private void Start()
    {
        Load();

        if (IsDestroyed)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe"))
        {
            StartCoroutine(OnBarrelDestroy());
        }
    }

    private IEnumerator OnBarrelDestroy()
    {
        //Play the VFX effect 
        VfxOnDestroy.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(0.2f);

        this.gameObject.SetActive(false);
        IsDestroyed = true;
        Save();
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new();
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Data"));
        }
        FileStream file = File.Create(string.Concat(string.Concat(Application.persistentDataPath, "/Data"), savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(string.Concat(Application.persistentDataPath, "/Data"), savePath)))
        {
            BinaryFormatter bf = new();
            FileStream file = File.Open(string.Concat(string.Concat(Application.persistentDataPath, "/Data"), savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

}
