using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using Crosstales.RTVoice;
using UnityEngine.UI;

public class SpeechRecognition : MonoBehaviour
{
    
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public GameObject RTVoice;
    public Text textToSpeak;

    private void Start()
    {
        // add key phrases to be spoken
        keywords.Add("show menu", () =>
        {
            OnSpokenShowMenu();
        });

        keywords.Add("hide menu", () =>
        {
            OnSpokenHideMenu();
        });

        keywords.Add("previous step", () =>
        {
            OnSpokenPrevStep();
        });
        keywords.Add("previous", () =>
        {
            OnSpokenPrevStep();
        });
        keywords.Add("step backward", () =>
        {
            OnSpokenPrevStep();
        });

        keywords.Add("next step", () =>
        {
            OnSpokenNextStep();
        });
        keywords.Add("next", () =>
        {
            OnSpokenNextStep();
        });
        keywords.Add("step forward", () =>
        {
            OnSpokenNextStep();
        });
        keywords.Add("okay next", () =>
        {
            OnSpokenNextStep();
        });
        keywords.Add("show next", () =>
        {
            OnSpokenNextStep();
        });
        keywords.Add("next please", () =>
        {
            OnSpokenNextStep();
        });

        /*
        keywords.Add("show live work density heatmap", () =>
        {
            OnSpokenDensityHeatmap();
        });
        keywords.Add("show live work density", () =>
        {
            OnSpokenDensityHeatmap();
        });
        keywords.Add("show density heatmap", () =>
        {
            OnSpokenDensityHeatmap();
        });
        keywords.Add("show density", () =>
        {
            OnSpokenDensityHeatmap();
        });
        keywords.Add("live work density heatmap", () =>
        {
            OnSpokenDensityHeatmap();
        });
        keywords.Add("live work density", () =>
        {
            OnSpokenDensityHeatmap();
        });
        keywords.Add("density heatmap", () =>
        {
            OnSpokenDensityHeatmap();
        });

        keywords.Add("show experience diversity heatmap", () =>
        {
            OnSpokenDiversityHeatmap();
        });
        keywords.Add("show experience diversity", () =>
        {
            OnSpokenDiversityHeatmap();
        });
        keywords.Add("show diversity heatmap", () =>
        {
            OnSpokenDiversityHeatmap();
        });
        keywords.Add("show diversity", () =>
        {
            OnSpokenDiversityHeatmap();
        });
        keywords.Add("experience diversity heatmap", () =>
        {
            OnSpokenDiversityHeatmap();
        });
        keywords.Add("experience diversity", () =>
        {
            OnSpokenDiversityHeatmap();
        });
        keywords.Add("diversity heatmap", () =>
        {
            OnSpokenDiversityHeatmap();
        });

        keywords.Add("show energy cost efficiency heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show energy cost efficiency", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show energy efficiency heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show cost efficiency heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show energy efficiency", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show cost efficiency", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show energy heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show cost heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show energy", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show cost", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("energy cost efficiency heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("energy cost efficiency", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("energy efficiency heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("cost efficiency heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("energy efficiency", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("cost efficiency", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("energy heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("cost heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });

        keywords.Add("show traffic performance heatmap", () =>
        {
            OnSpokenTrafficHeatmap();
        });
        keywords.Add("show traffic performance", () =>
        {
            OnSpokenTrafficHeatmap();
        });
        keywords.Add("show traffic heatmap", () =>
        {
            OnSpokenTrafficHeatmap();
        });
        keywords.Add("show traffic", () =>
        {
            OnSpokenTrafficHeatmap();
        });
        keywords.Add("traffic performance heatmap", () =>
        {
            OnSpokenTrafficHeatmap();
        });
        keywords.Add("traffic performance", () =>
        {
            OnSpokenTrafficHeatmap();
        });
        keywords.Add("traffic heatmap", () =>
        {
            OnSpokenTrafficHeatmap();
        });

        keywords.Add("show solar access performance heatmap", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("show solar access performance", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("show solar access", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("show solar performance heatmap", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("show solar performance", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("show solar heatmap", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("show solar", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("solar access performance heatmap", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("solar access performance", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("solar access", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("solar performance heatmap", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("solar performance", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("solar heatmap", () =>
        {
            OnSpokenSolarHeatmap();
        });

        keywords.Add("heatmap off", () =>
        {
            OnSpokenHeatmapOff();
        });
        keywords.Add("no heatmap", () =>
        {
            OnSpokenHeatmapOff();
        });
        keywords.Add("hide heatmap", () =>
        {
            OnSpokenHeatmapOff();
        });
        keywords.Add("visualization off", () =>
        {
            OnSpokenHeatmapOff();
        });
        keywords.Add("no visualization", () =>
        {
            OnSpokenHeatmapOff();
        });
        keywords.Add("hide visualization", () =>
        {
            OnSpokenHeatmapOff();
        });
        */

        // OnSpokenDensityHeatmap
        keywords.Add("show live work density heatmap", () =>
        {
            OnSpokenDensityHeatmap();
        });
        keywords.Add("show density heatmap", () =>
        {
            OnSpokenDensityHeatmap();
        });

        // OnSpokenDiversityHeatmap
        keywords.Add("show experience diversity heatmap", () =>
        {
            OnSpokenDiversityHeatmap();
        });
        keywords.Add("show diversity heatmap", () =>
        {
            OnSpokenDiversityHeatmap();
        });

        // OnSpokenEnergyCostHeatmap
        keywords.Add("show energy cost efficiency heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show energy efficiency heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show cost efficiency heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show energy heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });
        keywords.Add("show cost heatmap", () =>
        {
            OnSpokenEnergyCostHeatmap();
        });

        // OnSpokenTrafficHeatmap
        keywords.Add("show traffic performance heatmap", () =>
        {
            OnSpokenTrafficHeatmap();
        });
        keywords.Add("show traffic heatmap", () =>
        {
            OnSpokenTrafficHeatmap();
        });

        // OnSpokenSolarHeatmap
        keywords.Add("show solar access performance heatmap", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("show solar access heatmap", () =>
        {
            OnSpokenSolarHeatmap();
        });
        keywords.Add("show solar heatmap", () =>
        {
            OnSpokenSolarHeatmap();
        });

        // OnSpokenHeatmapOff
        keywords.Add("heatmap off", () =>
        {
            OnSpokenHeatmapOff();
        });
        keywords.Add("no heatmap", () =>
        {
            OnSpokenHeatmapOff();
        });
        keywords.Add("hide heatmap", () =>
        {
            OnSpokenHeatmapOff();
        });

        // OnSpokenImpolite
        keywords.Add("fuck", () =>
        {
            OnSpokenImpolite();
        });
        keywords.Add("what the fuck", () =>
        {
            OnSpokenImpolite();
        });
        keywords.Add("what the hell", () =>
        {
            OnSpokenImpolite();
        });
        keywords.Add("shit", () =>
        {
            OnSpokenImpolite();
        });

        // OnWelcomeRay
        keywords.Add("hi", () =>
        {
            //OnWelcomeRay();
            OnWelcome();
        });
        keywords.Add("hello", () =>
        {
            //OnWelcomeRay();
            OnWelcome();
        });
        keywords.Add("hey", () =>
        {
            //OnWelcomeRay();
            OnWelcome();
        });

        // OnSpokenHowAreYou
        keywords.Add("how are you", () =>
        {
            OnSpokenHowAreYou();
        });
        keywords.Add("what's up", () =>
        {
            OnSpokenHowAreYou();
        });
        keywords.Add("Sup", () =>
        {
            OnSpokenHowAreYou();
        });
        keywords.Add("how are you doing", () =>
        {
            OnSpokenHowAreYou();
        });
        keywords.Add("how are you recently", () =>
        {
            OnSpokenHowAreYou();
        });

        // define key word recognizer
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizerOnPhraseRecognized;
        keywordRecognizer.Start();

    }

    void KeywordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }

    }

    // define corespondent commands
    void OnSpokenShowMenu()
    {
        Debug.Log("OnSpokenShowMenu");
    }
    void OnSpokenHideMenu()
    {
        Debug.Log("OnSpokenHideMenu");
    }
    void OnSpokenPrevStep()
    {
        Debug.Log("OnSpokenPrevStep");
        if (GetComponent<AIStepCtrl>().intAIStep > -1)
        {
            GetComponent<AIStepCtrl>().intAIStep--;
        }
    }
    void OnSpokenNextStep()
    {
        Debug.Log("OnSpokenNextStep");
        if (GetComponent<AIStepCtrl>().intAIStep < 100)
        {
            GetComponent<AIStepCtrl>().intAIStep++;
        }
    }
    void OnSpokenDensityHeatmap()
    {
        Debug.Log("OnSpokenDensityHeatmap");
    }
    void OnSpokenDiversityHeatmap()
    {
        Debug.Log("OnSpokenDiversityHeatmap");
    }
    void OnSpokenEnergyCostHeatmap()
    {
        Debug.Log("OnSpokenEnergyCostHeatmap");
    }
    void OnSpokenTrafficHeatmap()
    {
        Debug.Log("OnSpokenTrafficHeatmap");
    }
    void OnSpokenSolarHeatmap()
    {
        Debug.Log("OnSpokenSolarHeatmap");
    }
    void OnSpokenHeatmapOff()
    {
        Debug.Log("OnSpokenHeatmapOff");
    }
    void OnSpokenImpolite()
    {
        Debug.Log("OnSpokenImpolite");
        float rnd = Random.Range(0.0f, 1.0f);
        string text;
        if (rnd < 0.5f)
        {
            text = "uh oh, you are not that friendly! ";
        }
        else
        {
            text = "Common courtesy is very much appreciated! ";
        }
        textToSpeak.text = text;
        RTVoice.GetComponent<LiveSpeaker>().Silence();
        RTVoice.GetComponent<LiveSpeaker>().Speak(text);
    }
    void OnWelcomeRay()
    {
        Debug.Log("OnWelcomeRay");
        float rnd = Random.Range(0.0f, 1.0f);
        string textSpeak;
        string textShow;
        if (rnd < 0.333f)
        {
            textSpeak = "hey there, welcome to my home, Mee Noree, Yasushi, and Ray! ";
            textShow = "hey there, welcome to my home, Minori, Yasushi, and Ray! ";
        }
        else if (rnd < 0.667f)
        {
            textSpeak = "Welcome, Mee Noree, Yasushi, and Ray! ";
            textShow = "Welcome, Minori, Yasushi, and Ray! ";
        }
        else
        {
            textSpeak = "Nice to meet you, Mee Noree, Yasushi, and Ray! ";
            textShow = "Nice to meet you, Minori, Yasushi, and Ray! ";
        }
        textToSpeak.text = textShow;
        RTVoice.GetComponent<LiveSpeaker>().Silence();
        RTVoice.GetComponent<LiveSpeaker>().Speak(textSpeak);
    }
    void OnWelcome()
    {
        Debug.Log("OnWelcome");
        float rnd = Random.Range(0.0f, 1.0f);
        string text;
        if (rnd < 0.333f)
        {
            text = "hey there, welcome to CityMatrix! ";
        }
        else if (rnd < 0.667f)
        {
            text = "Welcome! ";
        }
        else
        {
            text = "Nice to meet you! ";
        }
        textToSpeak.text = text;
        RTVoice.GetComponent<LiveSpeaker>().Silence();
        RTVoice.GetComponent<LiveSpeaker>().Speak(text);
    }
    void OnSpokenHowAreYou()
    {
        Debug.Log("OnSpokenHowAreYou");
        float rnd = Random.Range(0.0f, 1.0f);
        string text;
        if (rnd < 0.333f)
        {
            text = "I am doing well, how about you? ";
        }
        else if (rnd < 0.667f)
        {
            text = "Very good, may the force be with you! ";
        }
        else
        {
            text = "Fabulous! Thank you very much for asking! ";
        }
        textToSpeak.text = text;
        RTVoice.GetComponent<LiveSpeaker>().Silence();
        RTVoice.GetComponent<LiveSpeaker>().Speak(text);
    }

}
