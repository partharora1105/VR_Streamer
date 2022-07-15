using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Vimeo.Player;

public class manager : MonoBehaviour
{
    [SerializeField] private int numVideos;
    [SerializeField] private InputActionReference leftAction = null;
    [SerializeField] private InputActionReference rightAction = null;
    [SerializeField] private InputActionReference selectAction = null;
    [SerializeField] private Button[] buttons = new Button[3];
    [SerializeField] private RawImage[] images = new RawImage[3];
    [SerializeField] private GameObject room;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject loader;
    [SerializeField] private AudioSource clickAudio;
    [SerializeField] private AudioSource bgAudio;
    //[SerializeField] private VideoPlayer vid;
    [SerializeField] private VimeoPlayer player;
    private int videoIndex;
    private int selectedVideoIndex;
    private bool isStart;
    private bool isPlayed;
    public bool playOnlyFirstVideo;
    public string tempVideoUrl;


    private void Awake()
    {
        leftAction.action.started += goLeft;
        rightAction.action.started += goRight;
        selectAction.action.started += select;

    }
    private void Start()
    {
        selectedVideoIndex = -1;
        isStart = true;
        isPlayed = false;
        videoIndex = 0;
        generateImages(videoIndex);
        switchButton();
        player.Pause();
        //vid.Stop();
        isStart = false;
    }

    private void OnDestroy()
    {
        leftAction.action.started -= goLeft;
        rightAction.action.started -= goRight;
        selectAction.action.started -= select;
    }
    private void Update()
    {
        loader.SetActive(player.GetProgress() != 0);
    }

    private void goRight(InputAction.CallbackContext context)
    {
        if (videoIndex < numVideos - 1)
        {
            videoIndex++;
            switchButton();
        }

    }

    private void goLeft(InputAction.CallbackContext context)
    {
        if (videoIndex > 0)
        {
            videoIndex--;
            switchButton();
        }
    }

    private void select(InputAction.CallbackContext context)
    {
        ui.SetActive(isPlayed);
        room.SetActive(isPlayed);
        if (!isPlayed)
        {
            bgAudio.Pause();
            if (videoIndex == selectedVideoIndex)
            {
                player.Play();
                //vid.Play();
            } else
            {
                selectedVideoIndex = videoIndex;
                //playVideoOld();
                playVideo();
            }
        } else
        {
            bgAudio.Play();
            player.Pause();
            //vid.Pause();
        }
        isPlayed = !isPlayed;
    }

    public void selectTest()
    {
        ui.SetActive(isPlayed);
        room.SetActive(isPlayed);
        if (!isPlayed)
        {
            bgAudio.Pause();
            if (videoIndex == selectedVideoIndex)
            {
                player.Play();
                //vid.Play();
            }
            else
            {
                selectedVideoIndex = videoIndex;
                //playVideoOld();
                playVideo();
            }
        }
        else
        {
            bgAudio.Play();
            player.Pause();
            //vid.Pause();
        }
        isPlayed = !isPlayed;
    }

    private void playVideo()
    {
        player.vimeoVideoId = "728505578";
        //player.vimeoVideoId = "725205625";
        if (playOnlyFirstVideo)
        {
            //vid.clip = Resources.Load<VideoClip>("Videos/video0");
            //vid.url = tempVideoUrl;
            player.vimeoVideoId = "728505578";
            //player.PlayVideo("725205625");
        } else
        {
            //vid.clip = Resources.Load<VideoClip>("Videos/video" + videoIndex);
            //vid.url = tempVideoUrl;
            player.vimeoVideoId = "728505578";
            //player.PlayVideo("725205625");
        }
        player.Play();
        
        //vid.audioOutputMode = VideoAudioOutputMode.AudioSource;
        //vid.EnableAudioTrack(0, true);
        //vid.Prepare();
        //vid.Play();
    }
    //private void playVideoWithCounter()
    //{
    //    vid.clip = Resources.Load<VideoClip>("Videos/counter");
    //    vid.time += 25.0f;
    //    player.PlayVideo("725205625");
    //    vid.Play();
    //    Invoke("stopCounter", 6.0f);
        
    //}
    //private void stopCounter()
    //{
    //    vid.Stop();
    //    if (playOnlyFirstVideo)
    //    {
    //        vid.clip = Resources.Load<VideoClip>("Videos/video0");
    //    }
    //    else
    //    {
    //        vid.clip = Resources.Load<VideoClip>("Videos/video" + videoIndex);
    //    }
    //    vid.Play();
    //}

    private void switchButton()
    {
        if ((videoIndex == 0) || videoIndex % 3 == 0)
        {
            activateButton(0);
            generateImages(videoIndex);
        } else if ((videoIndex == 1) || videoIndex % 3 == 1)
        {
            activateButton(1);
        } else if ((videoIndex == 2) || videoIndex % 3 == 2)
        {
            activateButton(2);
            generateImages(videoIndex -  2);
        }
        if (!isStart) clickAudio.Play();
    }

    private void activateButton(int num)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (num == i)
            {
                ColorBlock cb = buttons[i].colors;
                cb.normalColor = Color.white;
                buttons[i].colors = cb;
                //activeButton = buttons[i];
            } else
            {
                ColorBlock cb = buttons[i].colors;
                cb.normalColor = Color.black;
                buttons[i].colors = cb;
            }
        }
    }

    private void generateImages(int startIndex)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if ((startIndex + i) < numVideos)
            {
                images[i].color = new Color(255, 255, 255, 255);
                images[i].texture = Resources.Load<Texture>("Thumbnails/img" + (startIndex + i));
            } else
            {
                images[i].color = new Color(0, 0, 0, 0);
            }
        }
    }



    


}
