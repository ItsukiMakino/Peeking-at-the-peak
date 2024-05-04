using UnityEngine;
using MemoryPack;
using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Security;
namespace MyGame
{
    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance;
        public static PlayerData loadData;
        public static string filePath { get; set; }
        public string fileName;
        CancellationToken token;
        public bool Initialize = false;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }

        }

        async void Start()
        {
            filePath = Path.Combine(Application.persistentDataPath, fileName);
            if (Initialize)
            {
                File.Delete(filePath);
            }
            loadData = await LoadAsync(filePath);
        }
        public static async UniTask<PlayerData> ResetData(bool Initialize = false, CancellationToken ct = default)
        {

            File.Delete(filePath);
            return loadData = await LoadAsync(filePath, ct);
        }
        public static async UniTask<PlayerData> LoadAsync(string filePath, CancellationToken ct = default)
        {
            if (File.Exists(filePath) is not true)
            {
                print("fileNotExist");
                return new PlayerData();
            }
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    return await MemoryPackSerializer.DeserializeAsync<PlayerData>(stream);
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            catch (SecurityException) { }

            return default;
        }
        public static async UniTask<bool> SaveAsync(string filePath, PlayerData data, CancellationToken ct = default)
        {
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    await MemoryPackSerializer.SerializeAsync(stream, data, cancellationToken: ct);

                return true;
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            catch (SecurityException) { }

            return false;
        }
        public void OnApplicationQuit()
        {


        }

    }
}