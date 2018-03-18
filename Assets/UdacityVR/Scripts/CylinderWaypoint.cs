using UnityEngine;
using System.Collections;

public class CylinderWaypoint : MonoBehaviour
{
    private enum State
    {
        Idle,
        Focused,
        Clicked,
        Approaching,
        Moving,
        Collect,
        Collected,
        Occupied,
        Open,
        Hidden
    }

    [SerializeField]
    private State _state = State.Idle;
    private Color _color_origional = new Color(0.0f, 1.0f, 0.0f, 0.5f);
    private Color _color = Color.white;
    private float _intensity = 1.0f; 
    private float _animated_lerp = 1.0f;
    private AudioSource _audio_source = null;
    private Material _material = null;
    private float _scale = 1.0f;

    [Header("Material")]
    public Material material = null;
    public Color color_hilight = new Color(0.8f, 0.8f, 1.0f, 0.125f);

    [Header("State Blend Speeds")]
    public float lerp_idle = 0.0f;
    public float lerp_focus = 0.0f;
    public float lerp_hide = 0.0f;
    public float lerp_clicked = 0.0f;

    [Header("State Animation Scales")]
    public float intensity_clicked_max = 15.0f;      
    public float intensity_animation = 1.0f;
    public float intensity_idle_min = 1.0f;     
    public float intensity_idle_max = 3.0f;
    public float intensity_focus_min = 4.0f;
    public float intensity_focus_max = 7.0f;

    [Header("Sounds")]
    public AudioClip clip_click = null;

    [Header("Hide Distance")]
    public float threshold = 0.125f;



    void Awake()
    {
        _material = Instantiate(material);
        _color_origional = _material.color;
        _color = _color_origional;
        _audio_source = gameObject.GetComponent<AudioSource>();
        _audio_source.clip = clip_click;
        _audio_source.playOnAwake = false;
    }

    void Update()
    {
        print(_scale);
        bool occupied = Camera.main.transform.parent.transform.position == gameObject.transform.position;

        switch (_state)
        {
            case State.Idle:
                Idle();

                _state = occupied ? State.Occupied : _state;
                break;

            case State.Focused:
                Focus();
                break;

                
            case State.Clicked:
                Clicked();
                bool intensitied = _intensity >= intensity_clicked_max * .95f;
                _state = intensitied ? State.Approaching : _state;
                break;

            case State.Approaching:
                Hide();
                _state = occupied ? State.Occupied : _state;

                break;

            case State.Occupied:
                Hide();

                _state = !occupied ? State.Idle : _state;
                break;

            case State.Hidden:
                Hide();
                break;

            default:
                break;
        }

        gameObject.GetComponentInChildren<MeshRenderer>().material.color = _color;
       
        gameObject.GetComponentInChildren<Light>().intensity = _intensity;

        gameObject.transform.localScale = Vector3.one * _scale;

        _animated_lerp = Mathf.Abs(Mathf.Cos(Time.time * intensity_animation));
    }


    public void Enter()
    {
        _state = _state == State.Idle ? State.Focused : _state;
    }


    public void Exit()
    {
        _state = State.Idle;
    }

    
    public void Click()
    {
        _state = _state == State.Focused ? State.Clicked : _state;

        _audio_source.Play();

        Camera.main.transform.parent.transform.position = gameObject.transform.position;
    }
    

    private void Idle()
    {
        _scale = 1.0f;
        float intensity = Mathf.Lerp(intensity_idle_min, intensity_idle_max, _animated_lerp);
        Color color = Color.Lerp(_color_origional, color_hilight, _animated_lerp);

        _intensity = Mathf.Lerp(_intensity, intensity, lerp_idle);
        
        _color = Color.Lerp(_color, color, lerp_idle);
    }


    public void Focus()
    {
        float intensity = Mathf.Lerp(intensity_focus_min, intensity_focus_max, _animated_lerp);
        Color color = Color.Lerp(_color_origional, color_hilight, _animated_lerp);

        _intensity = Mathf.Lerp(_intensity, intensity, lerp_focus);
        _color = Color.Lerp(_color, color, lerp_focus);
    }

    
    public void Clicked()
    {
        _intensity = Mathf.Lerp(_intensity, intensity_clicked_max, lerp_clicked);
        _color = Color.Lerp(_color, color_hilight, lerp_clicked);
    }


    public void Hide()
    {
        _intensity = Mathf.Lerp(_intensity, 0.0f, lerp_hide);
        _color = Color.Lerp(_color, Color.clear, lerp_hide);
        _scale = 0.0f;
    }

    public bool IsOccupied()
    {
        if (_state == State.Occupied)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
