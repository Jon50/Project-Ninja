using UnityEngine;

namespace TongueTwister.Utilities
{
    public static class LanguageUtility
    {
        /// <summary>
        /// Converts this <see cref="SystemLanguage"/> into an <see cref="ISO639Alpha2"/> value. Please note that is
        /// not entirely accurate, some system language values have more than one language code related to them.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static ISO639Alpha2 ToISO639Alpha2(this SystemLanguage language)
        {
            switch (language)
            {
                    case SystemLanguage.Afrikaans:
                        return ISO639Alpha2.AF;
                    case SystemLanguage.Arabic:
                        return ISO639Alpha2.AR;
                    case SystemLanguage.Belarusian:
                        return ISO639Alpha2.BE;
                    case SystemLanguage.Bulgarian:
                        return ISO639Alpha2.BG;
                    case SystemLanguage.Catalan:
                        return ISO639Alpha2.BS;
                    case SystemLanguage.Czech:
                        return ISO639Alpha2.CS;
                    case SystemLanguage.Danish:
                        return ISO639Alpha2.DA;
                    case SystemLanguage.German:
                        return ISO639Alpha2.DE;
                    case SystemLanguage.Greek:
                        return ISO639Alpha2.EL;
                    case SystemLanguage.English:
                        return ISO639Alpha2.EN;
                    case SystemLanguage.Spanish:
                        return ISO639Alpha2.ES;
                    case SystemLanguage.Estonian:
                        return ISO639Alpha2.ET;
                    case SystemLanguage.Basque:
                        return ISO639Alpha2.EU;
                    case SystemLanguage.Finnish:
                        return ISO639Alpha2.FI;
                    case SystemLanguage.Faroese:
                        return ISO639Alpha2.FO;
                    case SystemLanguage.French:
                        return ISO639Alpha2.FR;
                    case SystemLanguage.Hebrew:
                        return ISO639Alpha2.HE;
                    case SystemLanguage.Hungarian:
                        return ISO639Alpha2.HU;
                    case SystemLanguage.Indonesian:
                        return ISO639Alpha2.ID;
                    case SystemLanguage.Icelandic:
                        return ISO639Alpha2.IS;
                    case SystemLanguage.Italian:
                        return ISO639Alpha2.IT;
                    case SystemLanguage.Japanese:
                        return ISO639Alpha2.JA;
                    case SystemLanguage.Korean:
                        return ISO639Alpha2.KO;
                    case SystemLanguage.Lithuanian:
                        return ISO639Alpha2.LT;
                    case SystemLanguage.Latvian:
                        return ISO639Alpha2.LV;
                    case SystemLanguage.Dutch:
                        return ISO639Alpha2.NL;
                    case SystemLanguage.Norwegian:
                        return ISO639Alpha2.NO;
                    case SystemLanguage.Polish:
                        return ISO639Alpha2.PL;
                    case SystemLanguage.Portuguese:
                        return ISO639Alpha2.PT;
                    case SystemLanguage.Romanian:
                        return ISO639Alpha2.RO;
                    case SystemLanguage.Russian:
                        return ISO639Alpha2.RU;
                    case SystemLanguage.Slovak:
                        return ISO639Alpha2.SK;
                    case SystemLanguage.Slovenian:
                        return ISO639Alpha2.SL;
                    case SystemLanguage.SerboCroatian:
                        return ISO639Alpha2.SR;
                    case SystemLanguage.Swedish:
                        return ISO639Alpha2.SV;
                    case SystemLanguage.Thai:
                        return ISO639Alpha2.TH;
                    case SystemLanguage.Turkish:
                        return ISO639Alpha2.TR;
                    case SystemLanguage.Ukrainian:
                        return ISO639Alpha2.UK;
                    case SystemLanguage.Vietnamese:
                        return ISO639Alpha2.VI;
                    case SystemLanguage.Chinese:
                        return ISO639Alpha2.ZH;
            }

            return ISO639Alpha2.NONE;
        }
        
        /// <summary>
        /// Converts this ISO 639 Alpha 2 value into a <see cref="SystemLanguage"/> supported by the Unity Game Engine.
        /// Please note that not every ISO 639 Alpha 2 code maps to a system language. In some cases, the result may
        /// be <see cref="SystemLanguage.Unknown"/>.
        /// </summary>
        /// <param name="alpha2"></param>
        /// <returns></returns>
        public static SystemLanguage ToSystemLanguage(this ISO639Alpha2 alpha2)
        {
            switch (alpha2)
            {
                case ISO639Alpha2.AF:
                    return SystemLanguage.Afrikaans;
                case ISO639Alpha2.AR:
                    return SystemLanguage.Arabic;
                case ISO639Alpha2.BE:
                    return SystemLanguage.Belarusian;
                case ISO639Alpha2.BG:
                    return SystemLanguage.Bulgarian;
                case ISO639Alpha2.BS:
                    return SystemLanguage.Catalan;
                case ISO639Alpha2.CS:
                    return SystemLanguage.Czech;
                case ISO639Alpha2.DA:
                    return SystemLanguage.Danish;
                case ISO639Alpha2.DE:
                    return SystemLanguage.German;
                case ISO639Alpha2.EL:
                    return SystemLanguage.Greek;
                case ISO639Alpha2.EN:
                    return SystemLanguage.English;
                case ISO639Alpha2.ES:
                    return SystemLanguage.Spanish;
                case ISO639Alpha2.ET:
                    return SystemLanguage.Estonian;
                case ISO639Alpha2.EU:
                    return SystemLanguage.Basque;
                case ISO639Alpha2.FI:
                    return SystemLanguage.Finnish;
                case ISO639Alpha2.FO:
                    return SystemLanguage.Faroese;
                case ISO639Alpha2.FR:
                    return SystemLanguage.French;
                case ISO639Alpha2.HE:
                    return SystemLanguage.Hebrew;
                case ISO639Alpha2.HR:
                    return SystemLanguage.SerboCroatian;
                case ISO639Alpha2.HU:
                    return SystemLanguage.Hungarian;
                case ISO639Alpha2.ID:
                    return SystemLanguage.Indonesian;
                case ISO639Alpha2.IS:
                    return SystemLanguage.Icelandic;
                case ISO639Alpha2.IT:
                    return SystemLanguage.Italian;
                case ISO639Alpha2.JA:
                    return SystemLanguage.Japanese;
                case ISO639Alpha2.KO:
                    return SystemLanguage.Korean;
                case ISO639Alpha2.LT:
                    return SystemLanguage.Lithuanian;
                case ISO639Alpha2.LV:
                    return SystemLanguage.Latvian;
                case ISO639Alpha2.NB:
                    return SystemLanguage.Norwegian;
                case ISO639Alpha2.NL:
                    return SystemLanguage.Dutch;
                case ISO639Alpha2.NN:
                    return SystemLanguage.Norwegian;
                case ISO639Alpha2.NO:
                    return SystemLanguage.Norwegian;
                case ISO639Alpha2.PL:
                    return SystemLanguage.Polish;
                case ISO639Alpha2.PT:
                    return SystemLanguage.Portuguese;
                case ISO639Alpha2.RO:
                    return SystemLanguage.Romanian;
                case ISO639Alpha2.RU:
                    return SystemLanguage.Russian;
                case ISO639Alpha2.SK:
                    return SystemLanguage.Slovak;
                case ISO639Alpha2.SL:
                    return SystemLanguage.Slovenian;
                case ISO639Alpha2.SR:
                    return SystemLanguage.SerboCroatian;
                case ISO639Alpha2.SV:
                    return SystemLanguage.Swedish;
                case ISO639Alpha2.TH:
                    return SystemLanguage.Thai;
                case ISO639Alpha2.TR:
                    return SystemLanguage.Turkish;
                case ISO639Alpha2.UK:
                    return SystemLanguage.Ukrainian;
                case ISO639Alpha2.VI:
                    return SystemLanguage.Vietnamese;
                case ISO639Alpha2.ZH:
                    return SystemLanguage.Chinese;
            }

            return SystemLanguage.Unknown;
        }

        public static string GetEnglishName(ISO639Alpha2 alpha2)
        {
            switch (alpha2)
            {
                case ISO639Alpha2.AA: return "Afar";
                case ISO639Alpha2.AB: return "Abkhazian";
                case ISO639Alpha2.AE: return "Avestan";
                case ISO639Alpha2.AF: return "Afrikaans";
                case ISO639Alpha2.AK: return "Akan";
                case ISO639Alpha2.AM: return "Amharic";
                case ISO639Alpha2.AN: return "Aragonese";
                case ISO639Alpha2.AR: return "Arabic";
                case ISO639Alpha2.AS: return "Assamese";
                case ISO639Alpha2.AV: return "Avaric";
                case ISO639Alpha2.AY: return "Aymara";
                case ISO639Alpha2.AZ: return "Azerbaijani";
                case ISO639Alpha2.BA: return "Bashkir";
                case ISO639Alpha2.BE: return "Belarusian";
                case ISO639Alpha2.BG: return "Bulgarian";
                case ISO639Alpha2.BH: return "Bihari languages";
                case ISO639Alpha2.BI: return "Bislama";
                case ISO639Alpha2.BM: return "Bambara";
                case ISO639Alpha2.BN: return "Bengali";
                case ISO639Alpha2.BO: return "Tibetan";
                case ISO639Alpha2.BR: return "Breton";
                case ISO639Alpha2.BS: return "Bosnian";
                case ISO639Alpha2.CA: return "Catalan; Valencian";
                case ISO639Alpha2.CE: return "Chechen";
                case ISO639Alpha2.CH: return "Chamorro";
                case ISO639Alpha2.CO: return "Corsican";
                case ISO639Alpha2.CR: return "Cree";
                case ISO639Alpha2.CS: return "Czech";
                case ISO639Alpha2.CU: return "Church/Old Slavic";
                case ISO639Alpha2.CV: return "Chuvash";
                case ISO639Alpha2.CY: return "Welsh";
                case ISO639Alpha2.DA: return "Danish";
                case ISO639Alpha2.DE: return "German";
                case ISO639Alpha2.DV: return "Divehi; Dhivehi; Maldivian";
                case ISO639Alpha2.DZ: return "Dzongkha";
                case ISO639Alpha2.EE: return "Ewe";
                case ISO639Alpha2.EL: return "Greek, Modern (1453-)";
                case ISO639Alpha2.EN: return "English";
                case ISO639Alpha2.EO: return "Esperanto";
                case ISO639Alpha2.ES: return "Spanish; Castilian";
                case ISO639Alpha2.ET: return "Estonian";
                case ISO639Alpha2.EU: return "Basque";
                case ISO639Alpha2.FA: return "Persian";
                case ISO639Alpha2.FF: return "Fulah";
                case ISO639Alpha2.FI: return "Finnish";
                case ISO639Alpha2.FJ: return "Fijian";
                case ISO639Alpha2.FO: return "Faroese";
                case ISO639Alpha2.FR: return "French";
                case ISO639Alpha2.FY: return "Western Frisian";
                case ISO639Alpha2.GA: return "Irish";
                case ISO639Alpha2.GD: return "Gaelic; Scottish Gaelic";
                case ISO639Alpha2.GL: return "Galician";
                case ISO639Alpha2.GN: return "Guarani";
                case ISO639Alpha2.GU: return "Gujarati";
                case ISO639Alpha2.GV: return "Manx";
                case ISO639Alpha2.HA: return "Hausa";
                case ISO639Alpha2.HE: return "Hebrew";
                case ISO639Alpha2.HI: return "Hindi";
                case ISO639Alpha2.HO: return "Hiri Motu";
                case ISO639Alpha2.HR: return "Croatian";
                case ISO639Alpha2.HT: return "Haitian; Haitian Creole";
                case ISO639Alpha2.HU: return "Hungarian";
                case ISO639Alpha2.HY: return "Armenian";
                case ISO639Alpha2.HZ: return "Herero";
                case ISO639Alpha2.IA: return "Interlingua (IALA)";
                case ISO639Alpha2.ID: return "Indonesian";
                case ISO639Alpha2.IE: return "Interlingue; Occidental";
                case ISO639Alpha2.IG: return "Igbo";
                case ISO639Alpha2.II: return "Sichuan Yi; Nuosu";
                case ISO639Alpha2.IK: return "Inupiaq";
                case ISO639Alpha2.IO: return "Ido";
                case ISO639Alpha2.IS: return "Icelandic";
                case ISO639Alpha2.IT: return "Italian";
                case ISO639Alpha2.IU: return "Inuktitut";
                case ISO639Alpha2.JA: return "Japanese";
                case ISO639Alpha2.JV: return "Javanese";
                case ISO639Alpha2.KA: return "Georgian";
                case ISO639Alpha2.KG: return "Kongo";
                case ISO639Alpha2.KI: return "Kikuyu; Gikuyu";
                case ISO639Alpha2.KJ: return "Kuanyama; Kwanyama";
                case ISO639Alpha2.KK: return "Kazakh";
                case ISO639Alpha2.KL: return "Kalaallisut; Greenlandic";
                case ISO639Alpha2.KM: return "Central Khmer";
                case ISO639Alpha2.KN: return "Kannada";
                case ISO639Alpha2.KO: return "Korean";
                case ISO639Alpha2.KR: return "Kanuri";
                case ISO639Alpha2.KS: return "Kashmiri";
                case ISO639Alpha2.KU: return "Kurdish";
                case ISO639Alpha2.KV: return "Komi";
                case ISO639Alpha2.KW: return "Cornish";
                case ISO639Alpha2.KY: return "Kirghiz; Kyrgyz";
                case ISO639Alpha2.LA: return "Latin";
                case ISO639Alpha2.LB: return "Luxembourgish; Letzeburgesch";
                case ISO639Alpha2.LG: return "Ganda";
                case ISO639Alpha2.LI: return "Limburgan; Limburger; Limburgish";
                case ISO639Alpha2.LN: return "Lingala";
                case ISO639Alpha2.LO: return "Lao";
                case ISO639Alpha2.LT: return "Lithuanian";
                case ISO639Alpha2.LU: return "Luba-Katanga";
                case ISO639Alpha2.LV: return "Latvian";
                case ISO639Alpha2.MG: return "Malagasy";
                case ISO639Alpha2.MH: return "Marshallese";
                case ISO639Alpha2.MI: return "Maori";
                case ISO639Alpha2.MK: return "Macedonian";
                case ISO639Alpha2.ML: return "Malayalam";
                case ISO639Alpha2.MN: return "Mongolian";
                case ISO639Alpha2.MR: return "Marathi";
                case ISO639Alpha2.MS: return "Malay";
                case ISO639Alpha2.MT: return "Maltese";
                case ISO639Alpha2.MY: return "Burmese";
                case ISO639Alpha2.NA: return "Nauru";
                case ISO639Alpha2.NB: return "Bokmål, Norwegian; Norwegian Bokmål";
                case ISO639Alpha2.ND: return "Ndebele, North; North Ndebele";
                case ISO639Alpha2.NE: return "Nepali";
                case ISO639Alpha2.NG: return "Ndonga";
                case ISO639Alpha2.NL: return "Dutch; Flemish";
                case ISO639Alpha2.NN: return "Norwegian Nynorsk; Nynorsk, Norwegian";
                case ISO639Alpha2.NO: return "Norwegian";
                case ISO639Alpha2.NR: return "Ndebele, South; South Ndebele";
                case ISO639Alpha2.NV: return "Navajo; Navaho";
                case ISO639Alpha2.NY: return "Chichewa; Chewa; Nyanja";
                case ISO639Alpha2.OC: return "Occitan (post 1500)";
                case ISO639Alpha2.OJ: return "Ojibwa";
                case ISO639Alpha2.OM: return "Oromo";
                case ISO639Alpha2.OR: return "Oriya";
                case ISO639Alpha2.OS: return "Ossetian; Ossetic";
                case ISO639Alpha2.PA: return "Panjabi; Punjabi";
                case ISO639Alpha2.PI: return "Pali";
                case ISO639Alpha2.PL: return "Polish";
                case ISO639Alpha2.PS: return "Pushto; Pashto";
                case ISO639Alpha2.PT: return "Portuguese";
                case ISO639Alpha2.QU: return "Quechua";
                case ISO639Alpha2.RM: return "Romansh";
                case ISO639Alpha2.RN: return "Rundi";
                case ISO639Alpha2.RO: return "Romanian; Moldavian; Moldovan";
                case ISO639Alpha2.RU: return "Russian";
                case ISO639Alpha2.RW: return "Kinyarwanda";
                case ISO639Alpha2.SA: return "Sanskrit";
                case ISO639Alpha2.SC: return "Sardinian";
                case ISO639Alpha2.SD: return "Sindhi";
                case ISO639Alpha2.SE: return "Northern Sami";
                case ISO639Alpha2.SG: return "Sango";
                case ISO639Alpha2.SI: return "Sinhala; Sinhalese";
                case ISO639Alpha2.SK: return "Slovak";
                case ISO639Alpha2.SL: return "Slovenian";
                case ISO639Alpha2.SM: return "Samoan";
                case ISO639Alpha2.SN: return "Shona";
                case ISO639Alpha2.SO: return "Somali";
                case ISO639Alpha2.SQ: return "Albanian";
                case ISO639Alpha2.SR: return "Serbian";
                case ISO639Alpha2.SS: return "Swati";
                case ISO639Alpha2.ST: return "Sotho, Southern";
                case ISO639Alpha2.SU: return "Sundanese";
                case ISO639Alpha2.SV: return "Swedish";
                case ISO639Alpha2.SW: return "Swahili";
                case ISO639Alpha2.TA: return "Tamil";
                case ISO639Alpha2.TE: return "Telugu";
                case ISO639Alpha2.TG: return "Tajik";
                case ISO639Alpha2.TH: return "Thai";
                case ISO639Alpha2.TI: return "Tigrinya";
                case ISO639Alpha2.TK: return "Turkmen";
                case ISO639Alpha2.TL: return "Tagalog";
                case ISO639Alpha2.TN: return "Tswana";
                case ISO639Alpha2.TO: return "Tonga (Tonga Islands)";
                case ISO639Alpha2.TR: return "Turkish";
                case ISO639Alpha2.TS: return "Tsonga";
                case ISO639Alpha2.TT: return "Tatar";
                case ISO639Alpha2.TW: return "Twi";
                case ISO639Alpha2.TY: return "Tahitian";
                case ISO639Alpha2.UG: return "Uighur; Uyghur";
                case ISO639Alpha2.UK: return "Ukrainian";
                case ISO639Alpha2.UR: return "Urdu";
                case ISO639Alpha2.UZ: return "Uzbek";
                case ISO639Alpha2.VE: return "Venda";
                case ISO639Alpha2.VI: return "Vietnamese";
                case ISO639Alpha2.VO: return "Volapük";
                case ISO639Alpha2.WA: return "Walloon";
                case ISO639Alpha2.WO: return "Wolof";
                case ISO639Alpha2.XH: return "Xhosa";
                case ISO639Alpha2.YI: return "Yiddish";
                case ISO639Alpha2.YO: return "Yoruba";
                case ISO639Alpha2.ZA: return "Zhuang; Chuang";
                case ISO639Alpha2.ZH: return "Chinese";
                case ISO639Alpha2.ZU: return "Zulu";
            }

            return "";
        }
    }

    /// <summary>
    /// Represents a two letter alpha ISO 639 code.
    /// https://www.loc.gov/standards/iso639-2/php/code_list.php
    /// </summary>
    public enum ISO639Alpha2
    {
        /// <summary>
        /// Not supported or unknown ISO 639 Alpha 2 code.
        /// </summary>
        NONE,
        /// <summary>
        /// Afar
        /// </summary>
        AA,
        /// <summary>
        /// Abkhazian
        /// </summary>
        AB,
        /// <summary>
        /// Avestan
        /// </summary>
        AE,
        /// <summary>
        /// Afrikaans
        /// </summary>
        AF,
        /// <summary>
        /// Akan
        /// </summary>
        AK,
        /// <summary>
        /// Amharic
        /// </summary>
        AM,
        /// <summary>
        /// Aragonese
        /// </summary>
        AN,
        /// <summary>
        /// Arabic
        /// </summary>
        AR,
        /// <summary>
        /// Assamese
        /// </summary>
        AS,
        /// <summary>
        /// Avaric
        /// </summary>
        AV,
        /// <summary>
        /// Aymara
        /// </summary>
        AY,
        /// <summary>
        /// Azerbaijani
        /// </summary>
        AZ,
        /// <summary>
        /// Bashkir
        /// </summary>
        BA,
        /// <summary>
        /// Belarusian
        /// </summary>
        BE,
        /// <summary>
        /// Bulgarian
        /// </summary>
        BG,
        /// <summary>
        /// Bihari languages
        /// </summary>
        BH,
        /// <summary>
        /// Bislama
        /// </summary>
        BI,
        /// <summary>
        /// Bambara
        /// </summary>
        BM,
        /// <summary>
        /// Bengali
        /// </summary>
        BN,
        /// <summary>
        /// Tibetan
        /// </summary>
        BO,
        /// <summary>
        /// Breton
        /// </summary>
        BR,
        /// <summary>
        /// Bosnian
        /// </summary>
        BS,
        /// <summary>
        /// Catalan; Valencian
        /// </summary>
        CA,
        /// <summary>
        /// Chechen
        /// </summary>
        CE,
        /// <summary>
        /// Chamorro
        /// </summary>
        CH,
        /// <summary>
        /// Corsican
        /// </summary>
        CO,
        /// <summary>
        /// Cree
        /// </summary>
        CR,
        /// <summary>
        /// Czech
        /// </summary>
        CS,
        /// <summary>
        /// Church/Old Slavic
        /// </summary>
        CU,
        /// <summary>
        /// Chuvash
        /// </summary>
        CV,
        /// <summary>
        /// Welsh
        /// </summary>
        CY,
        /// <summary>
        /// Danish
        /// </summary>
        DA,
        /// <summary>
        /// German
        /// </summary>
        DE,
        /// <summary>
        /// Divehi; Dhivehi; Maldivian
        /// </summary>
        DV,
        /// <summary>
        /// Dzongkha
        /// </summary>
        DZ,
        /// <summary>
        /// Ewe
        /// </summary>
        EE,
        /// <summary>
        /// Greek, Modern (1453-)
        /// </summary>
        EL,
        /// <summary>
        /// English
        /// </summary>
        EN,
        /// <summary>
        /// Esperanto
        /// </summary>
        EO,
        /// <summary>
        /// Spanish; Castilian
        /// </summary>
        ES,
        /// <summary>
        /// Estonian
        /// </summary>
        ET,
        /// <summary>
        /// Basque
        /// </summary>
        EU,
        /// <summary>
        /// Persian
        /// </summary>
        FA,
        /// <summary>
        /// Fulah
        /// </summary>
        FF,
        /// <summary>
        /// Finnish
        /// </summary>
        FI,
        /// <summary>
        /// Fijian
        /// </summary>
        FJ,
        /// <summary>
        /// Faroese
        /// </summary>
        FO,
        /// <summary>
        /// French
        /// </summary>
        FR,
        /// <summary>
        /// Western Frisian
        /// </summary>
        FY,
        /// <summary>
        /// Irish
        /// </summary>
        GA,
        /// <summary>
        /// Gaelic; Scottish Gaelic
        /// </summary>
        GD,
        /// <summary>
        /// Galician
        /// </summary>
        GL,
        /// <summary>
        /// Guarani
        /// </summary>
        GN,
        /// <summary>
        /// Gujarati
        /// </summary>
        GU,
        /// <summary>
        /// Manx
        /// </summary>
        GV,
        /// <summary>
        /// Hausa
        /// </summary>
        HA,
        /// <summary>
        /// Hebrew
        /// </summary>
        HE,
        /// <summary>
        /// Hindi
        /// </summary>
        HI,
        /// <summary>
        /// Hiri Motu
        /// </summary>
        HO,
        /// <summary>
        /// Croatian
        /// </summary>
        HR,
        /// <summary>
        /// Haitian; Haitian Creole
        /// </summary>
        HT,
        /// <summary>
        /// Hungarian
        /// </summary>
        HU,
        /// <summary>
        /// Armenian
        /// </summary>
        HY,
        /// <summary>
        /// Herero
        /// </summary>
        HZ,
        /// <summary>
        /// Interlingua (IALA)
        /// </summary>
        IA,
        /// <summary>
        /// Indonesian
        /// </summary>
        ID,
        /// <summary>
        /// Interlingue; Occidental
        /// </summary>
        IE,
        /// <summary>
        /// Igbo
        /// </summary>
        IG,
        /// <summary>
        /// Sichuan Yi; Nuosu
        /// </summary>
        II,
        /// <summary>
        /// Inupiaq
        /// </summary>
        IK,
        /// <summary>
        /// Ido
        /// </summary>
        IO,
        /// <summary>
        /// Icelandic
        /// </summary>
        IS,
        /// <summary>
        /// Italian
        /// </summary>
        IT,
        /// <summary>
        /// Inuktitut
        /// </summary>
        IU,
        /// <summary>
        /// Japanese
        /// </summary>
        JA,
        /// <summary>
        /// Javanese
        /// </summary>
        JV,
        /// <summary>
        /// Georgian
        /// </summary>
        KA,
        /// <summary>
        /// Kongo
        /// </summary>
        KG,
        /// <summary>
        /// Kikuyu; Gikuyu
        /// </summary>
        KI,
        /// <summary>
        /// Kuanyama; Kwanyama
        /// </summary>
        KJ,
        /// <summary>
        /// Kazakh
        /// </summary>
        KK,
        /// <summary>
        /// Kalaallisut; Greenlandic
        /// </summary>
        KL,
        /// <summary>
        /// Central Khmer
        /// </summary>
        KM,
        /// <summary>
        /// Kannada
        /// </summary>
        KN,
        /// <summary>
        /// Korean
        /// </summary>
        KO,
        /// <summary>
        /// Kanuri
        /// </summary>
        KR,
        /// <summary>
        /// Kashmiri
        /// </summary>
        KS,
        /// <summary>
        /// Kurdish
        /// </summary>
        KU,
        /// <summary>
        /// Komi
        /// </summary>
        KV,
        /// <summary>
        /// Cornish
        /// </summary>
        KW,
        /// <summary>
        /// Kirghiz; Kyrgyz
        /// </summary>
        KY,
        /// <summary>
        /// Latin
        /// </summary>
        LA,
        /// <summary>
        /// Luxembourgish; Letzeburgesch
        /// </summary>
        LB,
        /// <summary>
        /// Ganda
        /// </summary>
        LG,
        /// <summary>
        /// Limburgan; Limburger; Limburgish
        /// </summary>
        LI,
        /// <summary>
        /// Lingala
        /// </summary>
        LN,
        /// <summary>
        /// Lao
        /// </summary>
        LO,
        /// <summary>
        /// Lithuanian
        /// </summary>
        LT,
        /// <summary>
        /// Luba-Katanga
        /// </summary>
        LU,
        /// <summary>
        /// Latvian
        /// </summary>
        LV,
        /// <summary>
        /// Malagasy
        /// </summary>
        MG,
        /// <summary>
        /// Marshallese
        /// </summary>
        MH,
        /// <summary>
        /// Maori
        /// </summary>
        MI,
        /// <summary>
        /// Macedonian
        /// </summary>
        MK,
        /// <summary>
        /// Malayalam
        /// </summary>
        ML,
        /// <summary>
        /// Mongolian
        /// </summary>
        MN,
        /// <summary>
        /// Marathi
        /// </summary>
        MR,
        /// <summary>
        /// Malay
        /// </summary>
        MS,
        /// <summary>
        /// Maltese
        /// </summary>
        MT,
        /// <summary>
        /// Burmese
        /// </summary>
        MY,
        /// <summary>
        /// Nauru
        /// </summary>
        NA,
        /// <summary>
        /// Bokmål, Norwegian; Norwegian Bokmål
        /// </summary>
        NB,
        /// <summary>
        /// Ndebele, North; North Ndebele
        /// </summary>
        ND,
        /// <summary>
        /// Nepali
        /// </summary>
        NE,
        /// <summary>
        /// Ndonga
        /// </summary>
        NG,
        /// <summary>
        /// Dutch; Flemish
        /// </summary>
        NL,
        /// <summary>
        /// Norwegian Nynorsk; Nynorsk, Norwegian
        /// </summary>
        NN,
        /// <summary>
        /// Norwegian
        /// </summary>
        NO,
        /// <summary>
        /// Ndebele, South; South Ndebele
        /// </summary>
        NR,
        /// <summary>
        /// Navajo; Navaho
        /// </summary>
        NV,
        /// <summary>
        /// Chichewa; Chewa; Nyanja
        /// </summary>
        NY,
        /// <summary>
        /// Occitan (post 1500)
        /// </summary>
        OC,
        /// <summary>
        /// Ojibwa
        /// </summary>
        OJ,
        /// <summary>
        /// Oromo
        /// </summary>
        OM,
        /// <summary>
        /// Oriya
        /// </summary>
        OR,
        /// <summary>
        /// Ossetian; Ossetic
        /// </summary>
        OS,
        /// <summary>
        /// Panjabi; Punjabi
        /// </summary>
        PA,
        /// <summary>
        /// Pali
        /// </summary>
        PI,
        /// <summary>
        /// Polish
        /// </summary>
        PL,
        /// <summary>
        /// Pushto; Pashto
        /// </summary>
        PS,
        /// <summary>
        /// Portuguese
        /// </summary>
        PT,
        /// <summary>
        /// Quechua
        /// </summary>
        QU,
        /// <summary>
        /// Romansh
        /// </summary>
        RM,
        /// <summary>
        /// Rundi
        /// </summary>
        RN,
        /// <summary>
        /// Romanian; Moldavian; Moldovan
        /// </summary>
        RO,
        /// <summary>
        /// Russian
        /// </summary>
        RU,
        /// <summary>
        /// Kinyarwanda
        /// </summary>
        RW,
        /// <summary>
        /// Sanskrit
        /// </summary>
        SA,
        /// <summary>
        /// Sardinian
        /// </summary>
        SC,
        /// <summary>
        /// Sindhi
        /// </summary>
        SD,
        /// <summary>
        /// Northern Sami
        /// </summary>
        SE,
        /// <summary>
        /// Sango
        /// </summary>
        SG,
        /// <summary>
        /// Sinhala; Sinhalese
        /// </summary>
        SI,
        /// <summary>
        /// Slovak
        /// </summary>
        SK,
        /// <summary>
        /// Slovenian
        /// </summary>
        SL,
        /// <summary>
        /// Samoan
        /// </summary>
        SM,
        /// <summary>
        /// Shona
        /// </summary>
        SN,
        /// <summary>
        /// Somali
        /// </summary>
        SO,
        /// <summary>
        /// Albanian
        /// </summary>
        SQ,
        /// <summary>
        /// Serbian
        /// </summary>
        SR,
        /// <summary>
        /// Swati
        /// </summary>
        SS,
        /// <summary>
        /// Sotho, Southern
        /// </summary>
        ST,
        /// <summary>
        /// Sundanese
        /// </summary>
        SU,
        /// <summary>
        /// Swedish
        /// </summary>
        SV,
        /// <summary>
        /// Swahili
        /// </summary>
        SW,
        /// <summary>
        /// Tamil
        /// </summary>
        TA,
        /// <summary>
        /// Telugu
        /// </summary>
        TE,
        /// <summary>
        /// Tajik
        /// </summary>
        TG,
        /// <summary>
        /// Thai
        /// </summary>
        TH,
        /// <summary>
        /// Tigrinya
        /// </summary>
        TI,
        /// <summary>
        /// Turkmen
        /// </summary>
        TK,
        /// <summary>
        /// Tagalog
        /// </summary>
        TL,
        /// <summary>
        /// Tswana
        /// </summary>
        TN,
        /// <summary>
        /// Tonga (Tonga Islands)
        /// </summary>
        TO,
        /// <summary>
        /// Turkish
        /// </summary>
        TR,
        /// <summary>
        /// Tsonga
        /// </summary>
        TS,
        /// <summary>
        /// Tatar
        /// </summary>
        TT,
        /// <summary>
        /// Twi
        /// </summary>
        TW,
        /// <summary>
        /// Tahitian
        /// </summary>
        TY,
        /// <summary>
        /// Uighur; Uyghur
        /// </summary>
        UG,
        /// <summary>
        /// Ukrainian
        /// </summary>
        UK,
        /// <summary>
        /// Urdu
        /// </summary>
        UR,
        /// <summary>
        /// Uzbek
        /// </summary>
        UZ,
        /// <summary>
        /// Venda
        /// </summary>
        VE,
        /// <summary>
        /// Vietnamese
        /// </summary>
        VI,
        /// <summary>
        /// Volapük
        /// </summary>
        VO,
        /// <summary>
        /// Walloon
        /// </summary>
        WA,
        /// <summary>
        /// Wolof
        /// </summary>
        WO,
        /// <summary>
        /// Xhosa
        /// </summary>
        XH,
        /// <summary>
        /// Yiddish
        /// </summary>
        YI,
        /// <summary>
        /// Yoruba
        /// </summary>
        YO,
        /// <summary>
        /// Zhuang; Chuang
        /// </summary>
        ZA,
        /// <summary>
        /// Chinese
        /// </summary>
        ZH,
        /// <summary>
        /// Zulu
        /// </summary>
        ZU,
    }
}