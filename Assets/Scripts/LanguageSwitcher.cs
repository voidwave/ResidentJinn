using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    [HideInInspector]
    public string[] GameDescription,
    Abilitiy0Title, Abilitiy1Title, Abilitiy2Title, Abilitiy3Title,
    Abilitiy0Desc, Abilitiy1Desc, Abilitiy2Desc, Abilitiy3Desc;

    public void Initilize()
    {

        GameDescription = new string[2];
        GameDescription[0] = "ﻢﻴﺣﺮﻟﺍ ﻦﻤﺣﺮﻟﺍ ﻪﻠﻟﺍ ﻢﺴﺑ \n!ﻪﻟﺰﻨﻣ ﻦﻣ نﺎﺴﻧﺍ فﻮﺨﻳ نﺍ ﺪﻳﺮﻳ ﻲﻨﺟ رﻭﺩ ﺐﻌﻠﺗ ﺖﻧﺃ \nزﻮﻔﺗ ﻒﻴﻛ \n%100 ﻰﻟﺍ ﻞﺼﻳ نﺎﺴﻧﻻﺍ فﻮﺧ دﺍﺪﻋ ﻞﻌﺟﺍ \n!ﺐﺋﺬﻟﺍ ﻚﻠﻛﺄﻳ ﻻﻭ";
        GameDescription[1] = "ﻢﻴﺣﺮﻟﺍ ﻦﻤﺣﺮﻟﺍ ﻪﻠﻟﺍ ﻢﺴﺑ \nYou Are a Jinn that wants to scare a Human out of YOUR Home! \n How to win? \n Get the human's fear meter >>>\nto reach 100% and don't die to the wolf!";

        //Titles
        Abilitiy0Title = new string[2];
        Abilitiy0Title[0] = "فﻮﺧ";
        Abilitiy0Title[1] = "Scare";

        Abilitiy1Title = new string[2];
        Abilitiy1Title[0] = "ﺮّﻴﺣ";
        Abilitiy1Title[1] = "Distract";

        Abilitiy2Title = new string[2];
        Abilitiy2Title[0] = "سﻮﺳﻭ";
        Abilitiy2Title[1] = "Tempt";

        Abilitiy3Title = new string[2];
        Abilitiy3Title[0] = "ﺮﺤﺳﺍ";
        Abilitiy3Title[1] = "Possess";

        //Descriptions
        Abilitiy0Desc = new string[2];
        Abilitiy0Desc[0] = "نﺎﺴﻧﻼﻟ ﻚﺑﺮﻗ ﻰﻠﻋ ﺪﻤﺘﻌﻳ . نﺎﺴﻧﻻﺍ فٍّﻮﺧ \n نﺎﺴﻧﻻﺍ فﻮﺧ ﺪﻳﺰﻳ+";
        Abilitiy0Desc[1] = "Scare the Human, depends on distance to Human.\n+Increases Human Fear";

        Abilitiy1Desc = new string[2];
        Abilitiy1Desc[0] = "ﻞﻈﻟﺍﺭﺎﻨﺑ ﺐﺋﺬﻟﺍ وﺍ نﺎﺴﻧﻻﺍ ﺮّﻴﺣ \n  نﺎﺴﻧﻻﺍ جﺍﺰﻣ ﺺﻘﻨﻳ-";
        Abilitiy1Desc[1] = "Distract The Human or The Wolf with ShadowFire. \n -Decreases Human Mood";

        Abilitiy2Desc = new string[2];
        Abilitiy2Desc[0] = "+%25 ﺐﻧﺬﻟﺍ دﺍﺪﻋ اﺫﺍ ,نﺎﺴﻧﻼﻟ سﻮﺳﻭ \n نﺎﺴﻧﻻﺍ بﻮﻧﺫ ﺪﻳﺰﻳ+";
        Abilitiy2Desc[1] = "Tempt The Human, if his Sin Meter is +25% \n +Increases Human Sin";

        Abilitiy3Desc = new string[2];
        Abilitiy3Desc[0] = "+%50 ﺐﻧﺬﻟﺍ و فﻮﺨﻟﺍ دﺍﺪﻋ اﺫﺍ ,ﻪﻴﻧﺎﺛ 30 ةﺪﻤﻟ ﻪﻴﻠﻋ ﺮﻄﻴﺳﻭ نﺎﺴﻧﻻﺍ ﺮﺤﺳﺍ \nنﺎﺴﻧﻻﺍ فﻮﺧ ﺪﻳﺰﻳ+\nنﺎﺴﻧﻻﺍ جﺍﺰﻣ ﺺﻘﻨﻳ-";
        Abilitiy3Desc[1] = "Gain Control of The Human for 30 seconds, if his Sin and Fear Meters are +50%\n+Increases Human Fear\n-Decreases Human Mood";
    }

}
