using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    // Biến được kết nối với Player trong trò chơi
    [SerializeField] Transform player;
    // Biến để hiển thị số quái vật đã tiêu diệt
    [SerializeField] TextMeshProUGUI killCountText;

    // Biến instance để duy trì một phiên bản duy nhất của EnemySpawner
    static EnemySpawner instance;
    // Danh sách để lưu trữ các quái vật
    List<GameObject> enemyList = new List<GameObject>(500);

    // Các hằng số để quy định vị trí tối đa của quái vật và thời gian giữa các lần xuất hiện
    const float maxX = 10;
    const float maxY = 16;
    const float DecreseSpawnDelayTime = 30f;

    // Thời gian giữa các lần xuất hiện
    float spawnDelay;
    // Giai đoạn hiện tại của trò chơi
    int stage;
    // Số quái vật đã tiêu diệt
    int killCount;

    // Hàm khởi tạo private để tránh tạo đối tượng mới bên ngoài
    private EnemySpawner() { }

    // Enum để định nghĩa hướng di chuyển của quái vật
    enum Direction
    {
        North,
        South,
        West,
        East
    }

    // Hàm được gọi khi đối tượng được khởi tạo
    void Awake()
    {
        Initialize();
    }

    // Hàm được gọi khi trò chơi bắt đầu
    void Start()
    {
        StartCoroutine(SpawnEnemy()); // Bắt đầu tạo quái vật
        StartCoroutine(listChecker()); // Bắt đầu kiểm tra danh sách quái vật
    }

    // Hàm khởi tạo EnemySpawner
    void Initialize()
    {
        instance = this;
        spawnDelay = 0.3f; // Thiết lập thời gian giữa các lần xuất hiện ban đầu
        stage = 1; // Giai đoạn ban đầu
        killCount = 0; // Số quái vật đã tiêu diệt ban đầu
    }

    // Coroutine để tạo quái vật
    IEnumerator SpawnEnemy()
    {
        GameObject newEnemy;

        while (true)
        {
            // Tạo quái vật dựa trên giai đoạn hiện tại của trò chơi
            switch (stage)
            {
                default:
                case 1:
                    newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.FlyingEye);
                    break;
                case 2:
                    newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Goblin);
                    break;
                case 3:
                    newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Mushroom);
                    break;
                case 4:
                    newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Skeleton);
                    break;
            }

            // Đặt vị trí của quái vật
            newEnemy.transform.position = RandomPosition();
            newEnemy.SetActive(true); // Kích hoạt quái vật
            enemyList.Add(newEnemy); // Thêm quái vật vào danh sách

            // Nếu giai đoạn là 5, tạo một quái vật FlyingEye khác
            if (stage == 5)
            {
                newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.FlyingEye);
                newEnemy.transform.position = RandomPosition();
                newEnemy.SetActive(true);
                enemyList.Add(newEnemy);
            }

            // Chờ một khoảng thời gian trước khi tạo quái vật tiếp theo
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Hàm để tạo vị trí ngẫu nhiên cho quái vật
    Vector3 RandomPosition()
    {
        Vector3 pos = new Vector3();

        // Chọn một hướng di chuyển ngẫu nhiên
        Direction direction = (Direction)Random.Range(0, 4);

        // Đặt vị trí dựa trên hướng di chuyển
        switch (direction)
        {
            case Direction.North:
                pos.x = Random.Range(player.transform.position.x - maxX, player.transform.position.x + maxX);
                pos.y = player.transform.position.y + 10f;
                break;
            case Direction.South:
                pos.x = Random.Range(player.transform.position.x - maxX, player.transform.position.x + maxX);
                pos.y = player.transform.position.y - 10f;
                break;
            case Direction.West:
                pos.x = player.transform.position.x - 16f;
                pos.y = Random.Range(player.transform.position.y - maxY, player.transform.position.y + maxY);
                break;
            case Direction.East:
                pos.x = player.transform.position.x + 15f;
                pos.y = Random.Range(player.transform.position.y - maxY, player.transform.position.y + maxY);
                break;
        }

        return pos;
    }

    // Hàm để lấy vị trí gần nhất của quái vật
    public Vector2 GetNearestEnemyPosition()
    {
        float[] min = { 0, int.MaxValue };

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (min[1] > (enemyList[i].transform.position - Player.GetInstance().GetPosition()).sqrMagnitude)
            {
                min[0] = i;
                min[1] = (enemyList[i].transform.position - Player.GetInstance().GetPosition()).sqrMagnitude;
            }
        }

        return enemyList[(int)min[0]].transform.position;
    }

    // Hàm để lấy vị trí ngẫu nhiên của quái vật
    public Vector2 GetRandomEnemyPosition()
    {
        int random = Random.Range(0, enemyList.Count);

        return enemyList[random].transform.position;
    }

    // Coroutine để kiểm tra danh sách quái vật
    IEnumerator listChecker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            // Kiểm tra danh sách quái vật và loại bỏ những quái vật không hoạt động
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].activeSelf)
                    enemyList.RemoveAt(i);
            }
        }
    }

    // Hàm để tăng giai đoạn của trò chơi
    public void IncreaseStage()
    {
        ++stage;

        // Nếu giai đoạn là 3 hoặc 4, giảm thời gian giữa các lần xuất hiện
        switch (stage)
        {
            case 3:
            case 4:
                spawnDelay *= DecreseSpawnDelayTime/2;
                break;
        }
    }

    // Hàm để tăng số quái vật đã tiêu diệt
    public void IncreaseKillCount()
    {
        ++killCount;

        // Cập nhật số quái vật đã tiêu diệt trên giao diện người dùng
        killCountText.text = killCount.ToString();
    }

    // Hàm để lấy phiên bản duy nhất của EnemySpawner
    public static EnemySpawner GetInstance()
    {
        return instance;
    }

    // Hàm để lấy số lượng quái vật trong danh sách
    public int GetListCount()
    {
        return enemyList.Count;
    }
}
