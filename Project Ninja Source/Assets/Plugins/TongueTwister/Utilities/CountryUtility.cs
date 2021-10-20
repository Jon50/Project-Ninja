namespace TongueTwister.Utilities
{
    public static class CountryUtility
    {
        /// <summary>
        /// Converts from the current <see cref="ISO3166Alpha3"/> value to the related <see cref="ISO3166Alpha2"/> value
        /// </summary>
        /// <param name="alpha3"></param>
        /// <returns></returns>
        public static ISO3166Alpha2 ToAlpha2(this ISO3166Alpha3 alpha3)
        {
            return (ISO3166Alpha2) (int) alpha3;
        }

        /// <summary>
        /// Converts from the current <see cref="ISO3166Alpha2"/> value to the related <see cref="ISO3166Alpha3"/> value
        /// </summary>
        /// <param name="alpha2"></param>
        /// <returns></returns>
        public static ISO3166Alpha3 ToAlpha3(this ISO3166Alpha2 alpha2)
        {
            return (ISO3166Alpha3) (int) alpha2;
        }

        /// <summary>
        /// Gets the friendly English name for the given ISO 3166 Alpha 2 code.
        /// </summary>
        /// <param name="alpha2"></param>
        /// <returns></returns>
        public static string GetEnglishName(ISO3166Alpha2 alpha2)
        {
            switch (alpha2)
            {
                   case ISO3166Alpha2.AF: return "Afghanistan";
                   case ISO3166Alpha2.AL: return "Albania";
                   case ISO3166Alpha2.DZ: return "Algeria";
                   case ISO3166Alpha2.AS: return "American Samoa";
                   case ISO3166Alpha2.AD: return "Andorra";
                   case ISO3166Alpha2.AO: return "Angola";
                   case ISO3166Alpha2.AI: return "Anguilla";
                   case ISO3166Alpha2.AQ: return "Antarctica";
                   case ISO3166Alpha2.AG: return "Antigua and Barbuda";
                   case ISO3166Alpha2.AR: return "Argentina";
                   case ISO3166Alpha2.AM: return "Armenia";
                   case ISO3166Alpha2.AW: return "Aruba";
                   case ISO3166Alpha2.AU: return "Australia";
                   case ISO3166Alpha2.AT: return "Austria";
                   case ISO3166Alpha2.AZ: return "Azerbaijan";
                   case ISO3166Alpha2.BS: return "Bahamas";
                   case ISO3166Alpha2.BH: return "Bahrain";
                   case ISO3166Alpha2.BD: return "Bangladesh";
                   case ISO3166Alpha2.BB: return "Barbados";
                   case ISO3166Alpha2.BY: return "Belarus";
                   case ISO3166Alpha2.BE: return "Belgium";
                   case ISO3166Alpha2.BZ: return "Belize";
                   case ISO3166Alpha2.BJ: return "Benin";
                   case ISO3166Alpha2.BM: return "Bermuda";
                   case ISO3166Alpha2.BT: return "Bhutan";
                   case ISO3166Alpha2.BO: return "Bolivia";
                   case ISO3166Alpha2.BQ: return "Bonaire, Sint Eustatius and Saba";
                   case ISO3166Alpha2.BA: return "Bosnia and Herzegovina";
                   case ISO3166Alpha2.BW: return "Botswana";
                   case ISO3166Alpha2.BV: return "Bouvet Island";
                   case ISO3166Alpha2.BR: return "Brazil";
                   case ISO3166Alpha2.IO: return "British Indian Ocean Territory";
                   case ISO3166Alpha2.BN: return "Brunei Darussalam";
                   case ISO3166Alpha2.BG: return "Bulgaria";
                   case ISO3166Alpha2.BF: return "Burkina Faso";
                   case ISO3166Alpha2.BI: return "Burundi";
                   case ISO3166Alpha2.CV: return "Cabo Verde";
                   case ISO3166Alpha2.KH: return "Cambodia";
                   case ISO3166Alpha2.CM: return "Cameroon";
                   case ISO3166Alpha2.CA: return "Canada";
                   case ISO3166Alpha2.KY: return "Cayman Islands";
                   case ISO3166Alpha2.CF: return "Central African Republic";
                   case ISO3166Alpha2.TD: return "Chad";
                   case ISO3166Alpha2.CL: return "Chile";
                   case ISO3166Alpha2.CN: return "China";
                   case ISO3166Alpha2.CX: return "Christmas Island";
                   case ISO3166Alpha2.CC: return "Cocos (Keeling) Islands";
                   case ISO3166Alpha2.CO: return "Colombia";
                   case ISO3166Alpha2.KM: return "Comoros";
                   case ISO3166Alpha2.CD: return "Congo (the Democratic Republic of the)";
                   case ISO3166Alpha2.CG: return "Congo";
                   case ISO3166Alpha2.CK: return "Cook Islands";
                   case ISO3166Alpha2.CR: return "Costa Rica";
                   case ISO3166Alpha2.HR: return "Croatia";
                   case ISO3166Alpha2.CU: return "Cuba";
                   case ISO3166Alpha2.CW: return "Curaçao";
                   case ISO3166Alpha2.CY: return "Cyprus";
                   case ISO3166Alpha2.CZ: return "Czechia";
                   case ISO3166Alpha2.CI: return "Côte d'Ivoire";
                   case ISO3166Alpha2.DK: return "Denmark";
                   case ISO3166Alpha2.DJ: return "Djibouti";
                   case ISO3166Alpha2.DM: return "Dominica";
                   case ISO3166Alpha2.DO: return "Dominican Republic";
                   case ISO3166Alpha2.EC: return "Ecuador";
                   case ISO3166Alpha2.EG: return "Egypt";
                   case ISO3166Alpha2.SV: return "El Salvador";
                   case ISO3166Alpha2.GQ: return "Equatorial Guinea";
                   case ISO3166Alpha2.ER: return "Eritrea";
                   case ISO3166Alpha2.EE: return "Estonia";
                   case ISO3166Alpha2.SZ: return "Eswatini";
                   case ISO3166Alpha2.ET: return "Ethiopia";
                   case ISO3166Alpha2.FK: return "Falkland Islands [Malvinas]";
                   case ISO3166Alpha2.FO: return "Faroe Islands";
                   case ISO3166Alpha2.FJ: return "Fiji";
                   case ISO3166Alpha2.FI: return "Finland";
                   case ISO3166Alpha2.FR: return "France";
                   case ISO3166Alpha2.GF: return "French Guiana";
                   case ISO3166Alpha2.PF: return "French Polynesia";
                   case ISO3166Alpha2.TF: return "French Southern Territories";
                   case ISO3166Alpha2.GA: return "Gabon";
                   case ISO3166Alpha2.GM: return "Gambia";
                   case ISO3166Alpha2.GE: return "Georgia";
                   case ISO3166Alpha2.DE: return "Germany";
                   case ISO3166Alpha2.GH: return "Ghana";
                   case ISO3166Alpha2.GI: return "Gibraltar";
                   case ISO3166Alpha2.GR: return "Greece";
                   case ISO3166Alpha2.GL: return "Greenland";
                   case ISO3166Alpha2.GD: return "Grenada";
                   case ISO3166Alpha2.GP: return "Guadeloupe";
                   case ISO3166Alpha2.GU: return "Guam";
                   case ISO3166Alpha2.GT: return "Guatemala";
                   case ISO3166Alpha2.GG: return "Guernsey";
                   case ISO3166Alpha2.GN: return "Guinea";
                   case ISO3166Alpha2.GW: return "Guinea-Bissau";
                   case ISO3166Alpha2.GY: return "Guyana";
                   case ISO3166Alpha2.HT: return "Haiti";
                   case ISO3166Alpha2.HM: return "Heard Island and McDonald Islands";
                   case ISO3166Alpha2.VA: return "Holy See";
                   case ISO3166Alpha2.HN: return "Honduras";
                   case ISO3166Alpha2.HK: return "Hong Kong";
                   case ISO3166Alpha2.HU: return "Hungary";
                   case ISO3166Alpha2.IS: return "Iceland";
                   case ISO3166Alpha2.IN: return "India";
                   case ISO3166Alpha2.ID: return "Indonesia";
                   case ISO3166Alpha2.IR: return "Iran (Islamic Republic of)";
                   case ISO3166Alpha2.IQ: return "Iraq";
                   case ISO3166Alpha2.IE: return "Ireland";
                   case ISO3166Alpha2.IM: return "Isle of Man";
                   case ISO3166Alpha2.IL: return "Israel";
                   case ISO3166Alpha2.IT: return "Italy";
                   case ISO3166Alpha2.JM: return "Jamaica";
                   case ISO3166Alpha2.JP: return "Japan";
                   case ISO3166Alpha2.JE: return "Jersey";
                   case ISO3166Alpha2.JO: return "Jordan";
                   case ISO3166Alpha2.KZ: return "Kazakhstan";
                   case ISO3166Alpha2.KE: return "Kenya";
                   case ISO3166Alpha2.KI: return "Kiribati";
                   case ISO3166Alpha2.KP: return "North Korea";
                   case ISO3166Alpha2.KR: return "South Korea";
                   case ISO3166Alpha2.KW: return "Kuwait";
                   case ISO3166Alpha2.KG: return "Kyrgyzstan";
                   case ISO3166Alpha2.LA: return "Lao People's Democratic Republic";
                   case ISO3166Alpha2.LV: return "Latvia";
                   case ISO3166Alpha2.LB: return "Lebanon";
                   case ISO3166Alpha2.LS: return "Lesotho";
                   case ISO3166Alpha2.LR: return "Liberia";
                   case ISO3166Alpha2.LY: return "Libya";
                   case ISO3166Alpha2.LI: return "Liechtenstein";
                   case ISO3166Alpha2.LT: return "Lithuania";
                   case ISO3166Alpha2.LU: return "Luxembourg";
                   case ISO3166Alpha2.MO: return "Macao";
                   case ISO3166Alpha2.MG: return "Madagascar";
                   case ISO3166Alpha2.MW: return "Malawi";
                   case ISO3166Alpha2.MY: return "Malaysia";
                   case ISO3166Alpha2.MV: return "Maldives";
                   case ISO3166Alpha2.ML: return "Mali";
                   case ISO3166Alpha2.MT: return "Malta";
                   case ISO3166Alpha2.MH: return "Marshall Islands";
                   case ISO3166Alpha2.MQ: return "Martinique";
                   case ISO3166Alpha2.MR: return "Mauritania";
                   case ISO3166Alpha2.MU: return "Mauritius";
                   case ISO3166Alpha2.YT: return "Mayotte";
                   case ISO3166Alpha2.MX: return "Mexico";
                   case ISO3166Alpha2.FM: return "Micronesia (Federated States of)";
                   case ISO3166Alpha2.MD: return "Moldova (the Republic of)";
                   case ISO3166Alpha2.MC: return "Monaco";
                   case ISO3166Alpha2.MN: return "Mongolia";
                   case ISO3166Alpha2.ME: return "Montenegro";
                   case ISO3166Alpha2.MS: return "Montserrat";
                   case ISO3166Alpha2.MA: return "Morocco";
                   case ISO3166Alpha2.MZ: return "Mozambique";
                   case ISO3166Alpha2.MM: return "Myanmar";
                   case ISO3166Alpha2.NA: return "Namibia";
                   case ISO3166Alpha2.NR: return "Nauru";
                   case ISO3166Alpha2.NP: return "Nepal";
                   case ISO3166Alpha2.NL: return "Netherlands";
                   case ISO3166Alpha2.NC: return "New Caledonia";
                   case ISO3166Alpha2.NZ: return "New Zealand";
                   case ISO3166Alpha2.NI: return "Nicaragua";
                   case ISO3166Alpha2.NE: return "Niger";
                   case ISO3166Alpha2.NG: return "Nigeria";
                   case ISO3166Alpha2.NU: return "Niue";
                   case ISO3166Alpha2.NF: return "Norfolk Island";
                   case ISO3166Alpha2.MK: return "North Macedonia";
                   case ISO3166Alpha2.MP: return "Northern Mariana Islands";
                   case ISO3166Alpha2.NO: return "Norway";
                   case ISO3166Alpha2.OM: return "Oman";
                   case ISO3166Alpha2.PK: return "Pakistan";
                   case ISO3166Alpha2.PW: return "Palau";
                   case ISO3166Alpha2.PS: return "Palestine, State of";
                   case ISO3166Alpha2.PA: return "Panama";
                   case ISO3166Alpha2.PG: return "Papua New Guinea";
                   case ISO3166Alpha2.PY: return "Paraguay";
                   case ISO3166Alpha2.PE: return "Peru";
                   case ISO3166Alpha2.PH: return "Philippines";
                   case ISO3166Alpha2.PN: return "Pitcairn";
                   case ISO3166Alpha2.PL: return "Poland";
                   case ISO3166Alpha2.PT: return "Portugal";
                   case ISO3166Alpha2.PR: return "Puerto Rico";
                   case ISO3166Alpha2.QA: return "Qatar";
                   case ISO3166Alpha2.RO: return "Romania";
                   case ISO3166Alpha2.RU: return "Russian Federation";
                   case ISO3166Alpha2.RW: return "Rwanda";
                   case ISO3166Alpha2.RE: return "Réunion";
                   case ISO3166Alpha2.BL: return "Saint Barthélemy";
                   case ISO3166Alpha2.SH: return "Saint Helena, Ascension and Tristan da Cunha";
                   case ISO3166Alpha2.KN: return "Saint Kitts and Nevis";
                   case ISO3166Alpha2.LC: return "Saint Lucia";
                   case ISO3166Alpha2.MF: return "Saint Martin (French part)";
                   case ISO3166Alpha2.PM: return "Saint Pierre and Miquelon";
                   case ISO3166Alpha2.VC: return "Saint Vincent and the Grenadines";
                   case ISO3166Alpha2.WS: return "Samoa";
                   case ISO3166Alpha2.SM: return "San Marino";
                   case ISO3166Alpha2.ST: return "Sao Tome and Principe";
                   case ISO3166Alpha2.SA: return "Saudi Arabia";
                   case ISO3166Alpha2.SN: return "Senegal";
                   case ISO3166Alpha2.RS: return "Serbia";
                   case ISO3166Alpha2.SC: return "Seychelles";
                   case ISO3166Alpha2.SL: return "Sierra Leone";
                   case ISO3166Alpha2.SG: return "Singapore";
                   case ISO3166Alpha2.SX: return "Sint Maarten (Dutch part)";
                   case ISO3166Alpha2.SK: return "Slovakia";
                   case ISO3166Alpha2.SI: return "Slovenia";
                   case ISO3166Alpha2.SB: return "Solomon Islands";
                   case ISO3166Alpha2.SO: return "Somalia";
                   case ISO3166Alpha2.ZA: return "South Africa";
                   case ISO3166Alpha2.GS: return "South Georgia and the South Sandwich Islands";
                   case ISO3166Alpha2.SS: return "South Sudan";
                   case ISO3166Alpha2.ES: return "Spain";
                   case ISO3166Alpha2.LK: return "Sri Lanka";
                   case ISO3166Alpha2.SD: return "Sudan";
                   case ISO3166Alpha2.SR: return "Suriname";
                   case ISO3166Alpha2.SJ: return "Svalbard and Jan Mayen";
                   case ISO3166Alpha2.SE: return "Sweden";
                   case ISO3166Alpha2.CH: return "Switzerland";
                   case ISO3166Alpha2.SY: return "Syrian Arab Republic";
                   case ISO3166Alpha2.TW: return "Taiwan (Province of China)";
                   case ISO3166Alpha2.TJ: return "Tajikistan";
                   case ISO3166Alpha2.TZ: return "Tanzania, the United Republic of";
                   case ISO3166Alpha2.TH: return "Thailand";
                   case ISO3166Alpha2.TL: return "Timor-Leste";
                   case ISO3166Alpha2.TG: return "Togo";
                   case ISO3166Alpha2.TK: return "Tokelau";
                   case ISO3166Alpha2.TO: return "Tonga";
                   case ISO3166Alpha2.TT: return "Trinidad and Tobago";
                   case ISO3166Alpha2.TN: return "Tunisia";
                   case ISO3166Alpha2.TR: return "Turkey";
                   case ISO3166Alpha2.TM: return "Turkmenistan";
                   case ISO3166Alpha2.TC: return "Turks and Caicos Islands";
                   case ISO3166Alpha2.TV: return "Tuvalu";
                   case ISO3166Alpha2.UG: return "Uganda";
                   case ISO3166Alpha2.UA: return "Ukraine";
                   case ISO3166Alpha2.AE: return "United Arab Emirates";
                   case ISO3166Alpha2.GB: return "United Kingdom of Great Britain and Northern Ireland";
                   case ISO3166Alpha2.UM: return "United States Minor Outlying Islands";
                   case ISO3166Alpha2.US: return "United States of America";
                   case ISO3166Alpha2.UY: return "Uruguay";
                   case ISO3166Alpha2.UZ: return "Uzbekistan";
                   case ISO3166Alpha2.VU: return "Vanuatu";
                   case ISO3166Alpha2.VE: return "Venezuela (Bolivarian Republic of)";
                   case ISO3166Alpha2.VN: return "Viet Nam";
                   case ISO3166Alpha2.VG: return "Virgin Islands (British)";
                   case ISO3166Alpha2.VI: return "Virgin Islands (U.S.)";
                   case ISO3166Alpha2.WF: return "Wallis and Futuna";
                   case ISO3166Alpha2.EH: return "Western Sahara*";
                   case ISO3166Alpha2.YE: return "Yemen";
                   case ISO3166Alpha2.ZM: return "Zambia";
                   case ISO3166Alpha2.ZW: return "Zimbabwe";
                   case ISO3166Alpha2.AX: return "Åland Islands";
            }
            
            return "";
        }

        /// <summary>
        /// Gets the friendly English name for the given ISO 3166 Alpha 3 code.
        /// </summary>
        /// <param name="alpha3"></param>
        /// <returns></returns>
        public static string GetEnglishName(ISO3166Alpha3 alpha3)
        {
            switch (alpha3)
            {
                case ISO3166Alpha3.AFG: return "Afghanistan";
                case ISO3166Alpha3.ALB: return "Albania";
                case ISO3166Alpha3.DZA: return "Algeria";
                case ISO3166Alpha3.ASM: return "American Samoa";
                case ISO3166Alpha3.AND: return "Andorra";
                case ISO3166Alpha3.AGO: return "Angola";
                case ISO3166Alpha3.AIA: return "Anguilla";
                case ISO3166Alpha3.ATA: return "Antarctica";
                case ISO3166Alpha3.ATG: return "Antigua and Barbuda";
                case ISO3166Alpha3.ARG: return "Argentina";
                case ISO3166Alpha3.ARM: return "Armenia";
                case ISO3166Alpha3.ABW: return "Aruba";
                case ISO3166Alpha3.AUS: return "Australia";
                case ISO3166Alpha3.AUT: return "Austria";
                case ISO3166Alpha3.AZE: return "Azerbaijan";
                case ISO3166Alpha3.BHS: return "Bahamas";
                case ISO3166Alpha3.BHR: return "Bahrain";
                case ISO3166Alpha3.BGD: return "Bangladesh";
                case ISO3166Alpha3.BRB: return "Barbados";
                case ISO3166Alpha3.BLR: return "Belarus";
                case ISO3166Alpha3.BEL: return "Belgium";
                case ISO3166Alpha3.BLZ: return "Belize";
                case ISO3166Alpha3.BEN: return "Benin";
                case ISO3166Alpha3.BMU: return "Bermuda";
                case ISO3166Alpha3.BTN: return "Bhutan";
                case ISO3166Alpha3.BOL: return "Bolivia (Plurinational State of)";
                case ISO3166Alpha3.BES: return "Bonaire, Sint Eustatius and Saba";
                case ISO3166Alpha3.BIH: return "Bosnia and Herzegovina";
                case ISO3166Alpha3.BWA: return "Botswana";
                case ISO3166Alpha3.BVT: return "Bouvet Island";
                case ISO3166Alpha3.BRA: return "Brazil";
                case ISO3166Alpha3.IOT: return "British Indian Ocean Territory";
                case ISO3166Alpha3.BRN: return "Brunei Darussalam";
                case ISO3166Alpha3.BGR: return "Bulgaria";
                case ISO3166Alpha3.BFA: return "Burkina Faso";
                case ISO3166Alpha3.BDI: return "Burundi";
                case ISO3166Alpha3.CPV: return "Cabo Verde";
                case ISO3166Alpha3.KHM: return "Cambodia";
                case ISO3166Alpha3.CMR: return "Cameroon";
                case ISO3166Alpha3.CAN: return "Canada";
                case ISO3166Alpha3.CYM: return "Cayman Islands";
                case ISO3166Alpha3.CAF: return "Central African Republic";
                case ISO3166Alpha3.TCD: return "Chad";
                case ISO3166Alpha3.CHL: return "Chile";
                case ISO3166Alpha3.CHN: return "China";
                case ISO3166Alpha3.CXR: return "Christmas Island";
                case ISO3166Alpha3.CCK: return "Cocos (Keeling) Islands";
                case ISO3166Alpha3.COL: return "Colombia";
                case ISO3166Alpha3.COM: return "Comoros";
                case ISO3166Alpha3.COD: return "Congo (the Democratic Republic of the)";
                case ISO3166Alpha3.COG: return "Congo";
                case ISO3166Alpha3.COK: return "Cook Islands";
                case ISO3166Alpha3.CRI: return "Costa Rica";
                case ISO3166Alpha3.HRV: return "Croatia";
                case ISO3166Alpha3.CUB: return "Cuba";
                case ISO3166Alpha3.CUW: return "Curaçao";
                case ISO3166Alpha3.CYP: return "Cyprus";
                case ISO3166Alpha3.CZE: return "Czechia";
                case ISO3166Alpha3.CIV: return "Côte d'Ivoire";
                case ISO3166Alpha3.DNK: return "Denmark";
                case ISO3166Alpha3.DJI: return "Djibouti";
                case ISO3166Alpha3.DMA: return "Dominica";
                case ISO3166Alpha3.DOM: return "Dominican Republic";
                case ISO3166Alpha3.ECU: return "Ecuador";
                case ISO3166Alpha3.EGY: return "Egypt";
                case ISO3166Alpha3.SLV: return "El Salvador";
                case ISO3166Alpha3.GNQ: return "Equatorial Guinea";
                case ISO3166Alpha3.ERI: return "Eritrea";
                case ISO3166Alpha3.EST: return "Estonia";
                case ISO3166Alpha3.SWZ: return "Eswatini";
                case ISO3166Alpha3.ETH: return "Ethiopia";
                case ISO3166Alpha3.FLK: return "Falkland Islands [Malvinas]";
                case ISO3166Alpha3.FRO: return "Faroe Islands";
                case ISO3166Alpha3.FJI: return "Fiji";
                case ISO3166Alpha3.FIN: return "Finland";
                case ISO3166Alpha3.FRA: return "France";
                case ISO3166Alpha3.GUF: return "French Guiana";
                case ISO3166Alpha3.PYF: return "French Polynesia";
                case ISO3166Alpha3.ATF: return "French Southern Territories";
                case ISO3166Alpha3.GAB: return "Gabon";
                case ISO3166Alpha3.GMB: return "Gambia";
                case ISO3166Alpha3.GEO: return "Georgia";
                case ISO3166Alpha3.DEU: return "Germany";
                case ISO3166Alpha3.GHA: return "Ghana";
                case ISO3166Alpha3.GIB: return "Gibraltar";
                case ISO3166Alpha3.GRC: return "Greece";
                case ISO3166Alpha3.GRL: return "Greenland";
                case ISO3166Alpha3.GRD: return "Grenada";
                case ISO3166Alpha3.GLP: return "Guadeloupe";
                case ISO3166Alpha3.GUM: return "Guam";
                case ISO3166Alpha3.GTM: return "Guatemala";
                case ISO3166Alpha3.GGY: return "Guernsey";
                case ISO3166Alpha3.GIN: return "Guinea";
                case ISO3166Alpha3.GNB: return "Guinea-Bissau";
                case ISO3166Alpha3.GUY: return "Guyana";
                case ISO3166Alpha3.HTI: return "Haiti";
                case ISO3166Alpha3.HMD: return "Heard Island and McDonald Islands";
                case ISO3166Alpha3.VAT: return "Holy See";
                case ISO3166Alpha3.HND: return "Honduras";
                case ISO3166Alpha3.HKG: return "Hong Kong";
                case ISO3166Alpha3.HUN: return "Hungary";
                case ISO3166Alpha3.ISL: return "Iceland";
                case ISO3166Alpha3.IND: return "India";
                case ISO3166Alpha3.IDN: return "Indonesia";
                case ISO3166Alpha3.IRN: return "Iran (Islamic Republic of)";
                case ISO3166Alpha3.IRQ: return "Iraq";
                case ISO3166Alpha3.IRL: return "Ireland";
                case ISO3166Alpha3.IMN: return "Isle of Man";
                case ISO3166Alpha3.ISR: return "Israel";
                case ISO3166Alpha3.ITA: return "Italy";
                case ISO3166Alpha3.JAM: return "Jamaica";
                case ISO3166Alpha3.JPN: return "Japan";
                case ISO3166Alpha3.JEY: return "Jersey";
                case ISO3166Alpha3.JOR: return "Jordan";
                case ISO3166Alpha3.KAZ: return "Kazakhstan";
                case ISO3166Alpha3.KEN: return "Kenya";
                case ISO3166Alpha3.KIR: return "Kiribati";
                case ISO3166Alpha3.PRK: return "Korea (the Democratic People's Republic of)";
                case ISO3166Alpha3.KOR: return "Korea (the Republic of)";
                case ISO3166Alpha3.KWT: return "Kuwait";
                case ISO3166Alpha3.KGZ: return "Kyrgyzstan";
                case ISO3166Alpha3.LAO: return "Lao People's Democratic Republic";
                case ISO3166Alpha3.LVA: return "Latvia";
                case ISO3166Alpha3.LBN: return "Lebanon";
                case ISO3166Alpha3.LSO: return "Lesotho";
                case ISO3166Alpha3.LBR: return "Liberia";
                case ISO3166Alpha3.LBY: return "Libya";
                case ISO3166Alpha3.LIE: return "Liechtenstein";
                case ISO3166Alpha3.LTU: return "Lithuania";
                case ISO3166Alpha3.LUX: return "Luxembourg";
                case ISO3166Alpha3.MAC: return "Macao";
                case ISO3166Alpha3.MDG: return "Madagascar";
                case ISO3166Alpha3.MWI: return "Malawi";
                case ISO3166Alpha3.MYS: return "Malaysia";
                case ISO3166Alpha3.MDV: return "Maldives";
                case ISO3166Alpha3.MLI: return "Mali";
                case ISO3166Alpha3.MLT: return "Malta";
                case ISO3166Alpha3.MHL: return "Marshall Islands";
                case ISO3166Alpha3.MTQ: return "Martinique";
                case ISO3166Alpha3.MRT: return "Mauritania";
                case ISO3166Alpha3.MUS: return "Mauritius";
                case ISO3166Alpha3.MYT: return "Mayotte";
                case ISO3166Alpha3.MEX: return "Mexico";
                case ISO3166Alpha3.FSM: return "Micronesia (Federated States of)";
                case ISO3166Alpha3.MDA: return "Moldova (the Republic of)";
                case ISO3166Alpha3.MCO: return "Monaco";
                case ISO3166Alpha3.MNG: return "Mongolia";
                case ISO3166Alpha3.MNE: return "Montenegro";
                case ISO3166Alpha3.MSR: return "Montserrat";
                case ISO3166Alpha3.MAR: return "Morocco";
                case ISO3166Alpha3.MOZ: return "Mozambique";
                case ISO3166Alpha3.MMR: return "Myanmar";
                case ISO3166Alpha3.NAM: return "Namibia";
                case ISO3166Alpha3.NRU: return "Nauru";
                case ISO3166Alpha3.NPL: return "Nepal";
                case ISO3166Alpha3.NLD: return "Netherlands";
                case ISO3166Alpha3.NCL: return "New Caledonia";
                case ISO3166Alpha3.NZL: return "New Zealand";
                case ISO3166Alpha3.NIC: return "Nicaragua";
                case ISO3166Alpha3.NER: return "Niger";
                case ISO3166Alpha3.NGA: return "Nigeria";
                case ISO3166Alpha3.NIU: return "Niue";
                case ISO3166Alpha3.NFK: return "Norfolk Island";
                case ISO3166Alpha3.MKD: return "North Macedonia";
                case ISO3166Alpha3.MNP: return "Northern Mariana Islands";
                case ISO3166Alpha3.NOR: return "Norway";
                case ISO3166Alpha3.OMN: return "Oman";
                case ISO3166Alpha3.PAK: return "Pakistan";
                case ISO3166Alpha3.PLW: return "Palau";
                case ISO3166Alpha3.PSE: return "Palestine, State of";
                case ISO3166Alpha3.PAN: return "Panama";
                case ISO3166Alpha3.PNG: return "Papua New Guinea";
                case ISO3166Alpha3.PRY: return "Paraguay";
                case ISO3166Alpha3.PER: return "Peru";
                case ISO3166Alpha3.PHL: return "Philippines";
                case ISO3166Alpha3.PCN: return "Pitcairn";
                case ISO3166Alpha3.POL: return "Poland";
                case ISO3166Alpha3.PRT: return "Portugal";
                case ISO3166Alpha3.PRI: return "Puerto Rico";
                case ISO3166Alpha3.QAT: return "Qatar";
                case ISO3166Alpha3.ROU: return "Romania";
                case ISO3166Alpha3.RUS: return "Russian Federation";
                case ISO3166Alpha3.RWA: return "Rwanda";
                case ISO3166Alpha3.REU: return "Réunion";
                case ISO3166Alpha3.BLM: return "Saint Barthélemy";
                case ISO3166Alpha3.SHN: return "Saint Helena, Ascension and Tristan da Cunha";
                case ISO3166Alpha3.KNA: return "Saint Kitts and Nevis";
                case ISO3166Alpha3.LCA: return "Saint Lucia";
                case ISO3166Alpha3.MAF: return "Saint Martin (French part)";
                case ISO3166Alpha3.SPM: return "Saint Pierre and Miquelon";
                case ISO3166Alpha3.VCT: return "Saint Vincent and the Grenadines";
                case ISO3166Alpha3.WSM: return "Samoa";
                case ISO3166Alpha3.SMR: return "San Marino";
                case ISO3166Alpha3.STP: return "Sao Tome and Principe";
                case ISO3166Alpha3.SAU: return "Saudi Arabia";
                case ISO3166Alpha3.SEN: return "Senegal";
                case ISO3166Alpha3.SRB: return "Serbia";
                case ISO3166Alpha3.SYC: return "Seychelles";
                case ISO3166Alpha3.SLE: return "Sierra Leone";
                case ISO3166Alpha3.SGP: return "Singapore";
                case ISO3166Alpha3.SXM: return "Sint Maarten (Dutch part)";
                case ISO3166Alpha3.SVK: return "Slovakia";
                case ISO3166Alpha3.SVN: return "Slovenia";
                case ISO3166Alpha3.SLB: return "Solomon Islands";
                case ISO3166Alpha3.SOM: return "Somalia";
                case ISO3166Alpha3.ZAF: return "South Africa";
                case ISO3166Alpha3.SGS: return "South Georgia and the South Sandwich Islands";
                case ISO3166Alpha3.SSD: return "South Sudan";
                case ISO3166Alpha3.ESP: return "Spain";
                case ISO3166Alpha3.LKA: return "Sri Lanka";
                case ISO3166Alpha3.SDN: return "Sudan";
                case ISO3166Alpha3.SUR: return "Suriname";
                case ISO3166Alpha3.SJM: return "Svalbard and Jan Mayen";
                case ISO3166Alpha3.SWE: return "Sweden";
                case ISO3166Alpha3.CHE: return "Switzerland";
                case ISO3166Alpha3.SYR: return "Syrian Arab Republic";
                case ISO3166Alpha3.TWN: return "Taiwan (Province of China)";
                case ISO3166Alpha3.TJK: return "Tajikistan";
                case ISO3166Alpha3.TZA: return "Tanzania, the United Republic of";
                case ISO3166Alpha3.THA: return "Thailand";
                case ISO3166Alpha3.TLS: return "Timor-Leste";
                case ISO3166Alpha3.TGO: return "Togo";
                case ISO3166Alpha3.TKL: return "Tokelau";
                case ISO3166Alpha3.TON: return "Tonga";
                case ISO3166Alpha3.TTO: return "Trinidad and Tobago";
                case ISO3166Alpha3.TUN: return "Tunisia";
                case ISO3166Alpha3.TUR: return "Turkey";
                case ISO3166Alpha3.TKM: return "Turkmenistan";
                case ISO3166Alpha3.TCA: return "Turks and Caicos Islands";
                case ISO3166Alpha3.TUV: return "Tuvalu";
                case ISO3166Alpha3.UGA: return "Uganda";
                case ISO3166Alpha3.UKR: return "Ukraine";
                case ISO3166Alpha3.ARE: return "United Arab Emirates";
                case ISO3166Alpha3.GBR: return "United Kingdom of Great Britain and Northern Ireland";
                case ISO3166Alpha3.UMI: return "United States Minor Outlying Islands";
                case ISO3166Alpha3.USA: return "United States of America";
                case ISO3166Alpha3.URY: return "Uruguay";
                case ISO3166Alpha3.UZB: return "Uzbekistan";
                case ISO3166Alpha3.VUT: return "Vanuatu";
                case ISO3166Alpha3.VEN: return "Venezuela (Bolivarian Republic of)";
                case ISO3166Alpha3.VNM: return "Viet Nam";
                case ISO3166Alpha3.VGB: return "Virgin Islands (British)";
                case ISO3166Alpha3.VIR: return "Virgin Islands (U.S.)";
                case ISO3166Alpha3.WLF: return "Wallis and Futuna";
                case ISO3166Alpha3.ESH: return "Western Sahara";
                case ISO3166Alpha3.YEM: return "Yemen";
                case ISO3166Alpha3.ZMB: return "Zambia";
                case ISO3166Alpha3.ZWE: return "Zimbabwe";
                case ISO3166Alpha3.ALA: return "Åland Islands";
            }
            
            return "";
        }
    }
    
    /// <summary>
    /// Represents a two letter alpha ISO 3166 code. The numeric value of each entry also represents its numeric
    /// ISO 3166 code.
    /// https://www.iso.org/iso-3166-country-codes.html
    /// </summary>
    public enum ISO3166Alpha2
    {
        /// <summary>
        /// The ISO 3166 Alpha 2 code is unavailable.
        /// </summary>
        NONE = 0,
        /// <summary>
        /// Afghanistan
        /// </summary>
        AF = 004,
        /// <summary>
        /// Albania
        /// </summary>
        AL = 008,
        /// <summary>
        /// Algeria
        /// </summary>
        DZ = 012,
        /// <summary>
        /// American Samoa
        /// </summary>
        AS = 016,
        /// <summary>
        /// Andorra
        /// </summary>
        AD = 020,
        /// <summary>
        /// Angola
        /// </summary>
        AO = 024,
        /// <summary>
        /// Anguilla
        /// </summary>
        AI = 660,
        /// <summary>
        /// Antarctica
        /// </summary>
        AQ = 010,
        /// <summary>
        /// Antigua and Barbuda
        /// </summary>
        AG = 028,
        /// <summary>
        /// Argentina
        /// </summary>
        AR = 032,
        /// <summary>
        /// Armenia
        /// </summary>
        AM = 051,
        /// <summary>
        /// Aruba
        /// </summary>
        AW = 533,
        /// <summary>
        /// Australia
        /// </summary>
        AU = 036,
        /// <summary>
        /// Austria
        /// </summary>
        AT = 040,
        /// <summary>
        /// Azerbaijan
        /// </summary>
        AZ = 031,
        /// <summary>
        /// Bahamas
        /// </summary>
        BS = 044,
        /// <summary>
        /// Bahrain
        /// </summary>
        BH = 048,
        /// <summary>
        /// Bangladesh
        /// </summary>
        BD = 050,
        /// <summary>
        /// Barbados
        /// </summary>
        BB = 052,
        /// <summary>
        /// Belarus
        /// </summary>
        BY = 112,
        /// <summary>
        /// Belgium
        /// </summary>
        BE = 056,
        /// <summary>
        /// Belize
        /// </summary>
        BZ = 084,
        /// <summary>
        /// Benin
        /// </summary>
        BJ = 204,
        /// <summary>
        /// Bermuda
        /// </summary>
        BM = 060,
        /// <summary>
        /// Bhutan
        /// </summary>
        BT = 064,
        /// <summary>
        /// Bolivia (Plurinational State of)
        /// </summary>
        BO = 068,
        /// <summary>
        /// Bonaire, Sint Eustatius and Saba
        /// </summary>
        BQ = 535,
        /// <summary>
        /// Bosnia and Herzegovina
        /// </summary>
        BA = 070,
        /// <summary>
        /// Botswana
        /// </summary>
        BW = 072,
        /// <summary>
        /// Bouvet Island
        /// </summary>
        BV = 074,
        /// <summary>
        /// Brazil
        /// </summary>
        BR = 076,
        /// <summary>
        /// British Indian Ocean Territory
        /// </summary>
        IO = 086,
        /// <summary>
        /// Brunei Darussalam
        /// </summary>
        BN = 096,
        /// <summary>
        /// Bulgaria
        /// </summary>
        BG = 100,
        /// <summary>
        /// Burkina Faso
        /// </summary>
        BF = 854,
        /// <summary>
        /// Burundi
        /// </summary>
        BI = 108,
        /// <summary>
        /// Cabo Verde
        /// </summary>
        CV = 132,
        /// <summary>
        /// Cambodia
        /// </summary>
        KH = 116,
        /// <summary>
        /// Cameroon
        /// </summary>
        CM = 120,
        /// <summary>
        /// Canada
        /// </summary>
        CA = 124,
        /// <summary>
        /// Cayman Islands
        /// </summary>
        KY = 136,
        /// <summary>
        /// Central African Republic
        /// </summary>
        CF = 140,
        /// <summary>
        /// Chad
        /// </summary>
        TD = 148,
        /// <summary>
        /// Chile
        /// </summary>
        CL = 152,
        /// <summary>
        /// China
        /// </summary>
        CN = 156,
        /// <summary>
        /// Christmas Island
        /// </summary>
        CX = 162,
        /// <summary>
        /// Cocos (Keeling) Islands
        /// </summary>
        CC = 166,
        /// <summary>
        /// Colombia
        /// </summary>
        CO = 170,
        /// <summary>
        /// Comoros
        /// </summary>
        KM = 174,
        /// <summary>
        /// Congo (the Democratic Republic of the)
        /// </summary>
        CD = 180,
        /// <summary>
        /// Congo
        /// </summary>
        CG = 178,
        /// <summary>
        /// Cook Islands
        /// </summary>
        CK = 184,
        /// <summary>
        /// Costa Rica
        /// </summary>
        CR = 188,
        /// <summary>
        /// Croatia
        /// </summary>
        HR = 191,
        /// <summary>
        /// Cuba
        /// </summary>
        CU = 192,
        /// <summary>
        /// Curaçao
        /// </summary>
        CW = 531,
        /// <summary>
        /// Cyprus
        /// </summary>
        CY = 196,
        /// <summary>
        /// Czechia
        /// </summary>
        CZ = 203,
        /// <summary>
        /// Côte d'Ivoire
        /// </summary>
        CI = 384,
        /// <summary>
        /// Denmark
        /// </summary>
        DK = 208,
        /// <summary>
        /// Djibouti
        /// </summary>
        DJ = 262,
        /// <summary>
        /// Dominica
        /// </summary>
        DM = 212,
        /// <summary>
        /// Dominican Republic
        /// </summary>
        DO = 214,
        /// <summary>
        /// Ecuador
        /// </summary>
        EC = 218,
        /// <summary>
        /// Egypt
        /// </summary>
        EG = 818,
        /// <summary>
        /// El Salvador
        /// </summary>
        SV = 222,
        /// <summary>
        /// Equatorial Guinea
        /// </summary>
        GQ = 226,
        /// <summary>
        /// Eritrea
        /// </summary>
        ER = 232,
        /// <summary>
        /// Estonia
        /// </summary>
        EE = 233,
        /// <summary>
        /// Eswatini
        /// </summary>
        SZ = 748,
        /// <summary>
        /// Ethiopia
        /// </summary>
        ET = 231,
        /// <summary>
        /// Falkland Islands [Malvinas]
        /// </summary>
        FK = 238,
        /// <summary>
        /// Faroe Islands
        /// </summary>
        FO = 234,
        /// <summary>
        /// Fiji
        /// </summary>
        FJ = 242,
        /// <summary>
        /// Finland
        /// </summary>
        FI = 246,
        /// <summary>
        /// France
        /// </summary>
        FR = 250,
        /// <summary>
        /// French Guiana
        /// </summary>
        GF = 254,
        /// <summary>
        /// French Polynesia
        /// </summary>
        PF = 258,
        /// <summary>
        /// French Southern Territories
        /// </summary>
        TF = 260,
        /// <summary>
        /// Gabon
        /// </summary>
        GA = 266,
        /// <summary>
        /// Gambia
        /// </summary>
        GM = 270,
        /// <summary>
        /// Georgia
        /// </summary>
        GE = 268,
        /// <summary>
        /// Germany
        /// </summary>
        DE = 276,
        /// <summary>
        /// Ghana
        /// </summary>
        GH = 288,
        /// <summary>
        /// Gibraltar
        /// </summary>
        GI = 292,
        /// <summary>
        /// Greece
        /// </summary>
        GR = 300,
        /// <summary>
        /// Greenland
        /// </summary>
        GL = 304,
        /// <summary>
        /// Grenada
        /// </summary>
        GD = 308,
        /// <summary>
        /// Guadeloupe
        /// </summary>
        GP = 312,
        /// <summary>
        /// Guam
        /// </summary>
        GU = 316,
        /// <summary>
        /// Guatemala
        /// </summary>
        GT = 320,
        /// <summary>
        /// Guernsey
        /// </summary>
        GG = 831,
        /// <summary>
        /// Guinea
        /// </summary>
        GN = 324,
        /// <summary>
        /// Guinea-Bissau
        /// </summary>
        GW = 624,
        /// <summary>
        /// Guyana
        /// </summary>
        GY = 328,
        /// <summary>
        /// Haiti
        /// </summary>
        HT = 332,
        /// <summary>
        /// Heard Island and McDonald Islands
        /// </summary>
        HM = 334,
        /// <summary>
        /// Holy See
        /// </summary>
        VA = 336,
        /// <summary>
        /// Honduras
        /// </summary>
        HN = 340,
        /// <summary>
        /// Hong Kong
        /// </summary>
        HK = 344,
        /// <summary>
        /// Hungary
        /// </summary>
        HU = 348,
        /// <summary>
        /// Iceland
        /// </summary>
        IS = 352,
        /// <summary>
        /// India
        /// </summary>
        IN = 356,
        /// <summary>
        /// Indonesia
        /// </summary>
        ID = 360,
        /// <summary>
        /// Iran (Islamic Republic of)
        /// </summary>
        IR = 364,
        /// <summary>
        /// Iraq
        /// </summary>
        IQ = 368,
        /// <summary>
        /// Ireland
        /// </summary>
        IE = 372,
        /// <summary>
        /// Isle of Man
        /// </summary>
        IM = 833,
        /// <summary>
        /// Israel
        /// </summary>
        IL = 376,
        /// <summary>
        /// Italy
        /// </summary>
        IT = 380,
        /// <summary>
        /// Jamaica
        /// </summary>
        JM = 388,
        /// <summary>
        /// Japan
        /// </summary>
        JP = 392,
        /// <summary>
        /// Jersey
        /// </summary>
        JE = 832,
        /// <summary>
        /// Jordan
        /// </summary>
        JO = 400,
        /// <summary>
        /// Kazakhstan
        /// </summary>
        KZ = 398,
        /// <summary>
        /// Kenya
        /// </summary>
        KE = 404,
        /// <summary>
        /// Kiribati
        /// </summary>
        KI = 296,
        /// <summary>
        /// Korea (the Democratic People's Republic of)
        /// </summary>
        KP = 408,
        /// <summary>
        /// Korea (the Republic of)
        /// </summary>
        KR = 410,
        /// <summary>
        /// Kuwait
        /// </summary>
        KW = 414,
        /// <summary>
        /// Kyrgyzstan
        /// </summary>
        KG = 417,
        /// <summary>
        /// Lao People's Democratic Republic
        /// </summary>
        LA = 418,
        /// <summary>
        /// Latvia
        /// </summary>
        LV = 428,
        /// <summary>
        /// Lebanon
        /// </summary>
        LB = 422,
        /// <summary>
        /// Lesotho
        /// </summary>
        LS = 426,
        /// <summary>
        /// Liberia
        /// </summary>
        LR = 430,
        /// <summary>
        /// Libya
        /// </summary>
        LY = 434,
        /// <summary>
        /// Liechtenstein
        /// </summary>
        LI = 438,
        /// <summary>
        /// Lithuania
        /// </summary>
        LT = 440,
        /// <summary>
        /// Luxembourg
        /// </summary>
        LU = 442,
        /// <summary>
        /// Macao
        /// </summary>
        MO = 446,
        /// <summary>
        /// Madagascar
        /// </summary>
        MG = 450,
        /// <summary>
        /// Malawi
        /// </summary>
        MW = 454,
        /// <summary>
        /// Malaysia
        /// </summary>
        MY = 458,
        /// <summary>
        /// Maldives
        /// </summary>
        MV = 462,
        /// <summary>
        /// Mali
        /// </summary>
        ML = 466,
        /// <summary>
        /// Malta
        /// </summary>
        MT = 470,
        /// <summary>
        /// Marshall Islands
        /// </summary>
        MH = 584,
        /// <summary>
        /// Martinique
        /// </summary>
        MQ = 474,
        /// <summary>
        /// Mauritania
        /// </summary>
        MR = 478,
        /// <summary>
        /// Mauritius
        /// </summary>
        MU = 480,
        /// <summary>
        /// Mayotte
        /// </summary>
        YT = 175,
        /// <summary>
        /// Mexico
        /// </summary>
        MX = 484,
        /// <summary>
        /// Micronesia (Federated States of)
        /// </summary>
        FM = 583,
        /// <summary>
        /// Moldova (the Republic of)
        /// </summary>
        MD = 498,
        /// <summary>
        /// Monaco
        /// </summary>
        MC = 492,
        /// <summary>
        /// Mongolia
        /// </summary>
        MN = 496,
        /// <summary>
        /// Montenegro
        /// </summary>
        ME = 499,
        /// <summary>
        /// Montserrat
        /// </summary>
        MS = 500,
        /// <summary>
        /// Morocco
        /// </summary>
        MA = 504,
        /// <summary>
        /// Mozambique
        /// </summary>
        MZ = 508,
        /// <summary>
        /// Myanmar
        /// </summary>
        MM = 104,
        /// <summary>
        /// Namibia
        /// </summary>
        NA = 516,
        /// <summary>
        /// Nauru
        /// </summary>
        NR = 520,
        /// <summary>
        /// Nepal
        /// </summary>
        NP = 524,
        /// <summary>
        /// Netherlands
        /// </summary>
        NL = 528,
        /// <summary>
        /// New Caledonia
        /// </summary>
        NC = 540,
        /// <summary>
        /// New Zealand
        /// </summary>
        NZ = 554,
        /// <summary>
        /// Nicaragua
        /// </summary>
        NI = 558,
        /// <summary>
        /// Niger
        /// </summary>
        NE = 562,
        /// <summary>
        /// Nigeria
        /// </summary>
        NG = 566,
        /// <summary>
        /// Niue
        /// </summary>
        NU = 570,
        /// <summary>
        /// Norfolk Island
        /// </summary>
        NF = 574,
        /// <summary>
        /// North Macedonia
        /// </summary>
        MK = 807,
        /// <summary>
        /// Northern Mariana Islands
        /// </summary>
        MP = 580,
        /// <summary>
        /// Norway
        /// </summary>
        NO = 578,
        /// <summary>
        /// Oman
        /// </summary>
        OM = 512,
        /// <summary>
        /// Pakistan
        /// </summary>
        PK = 586,
        /// <summary>
        /// Palau
        /// </summary>
        PW = 585,
        /// <summary>
        /// Palestine, State of
        /// </summary>
        PS = 275,
        /// <summary>
        /// Panama
        /// </summary>
        PA = 591,
        /// <summary>
        /// Papua New Guinea
        /// </summary>
        PG = 598,
        /// <summary>
        /// Paraguay
        /// </summary>
        PY = 600,
        /// <summary>
        /// Peru
        /// </summary>
        PE = 604,
        /// <summary>
        /// Philippines
        /// </summary>
        PH = 608,
        /// <summary>
        /// Pitcairn
        /// </summary>
        PN = 612,
        /// <summary>
        /// Poland
        /// </summary>
        PL = 616,
        /// <summary>
        /// Portugal
        /// </summary>
        PT = 620,
        /// <summary>
        /// Puerto Rico
        /// </summary>
        PR = 630,
        /// <summary>
        /// Qatar
        /// </summary>
        QA = 634,
        /// <summary>
        /// Romania
        /// </summary>
        RO = 642,
        /// <summary>
        /// Russian Federation
        /// </summary>
        RU = 643,
        /// <summary>
        /// Rwanda
        /// </summary>
        RW = 646,
        /// <summary>
        /// Réunion
        /// </summary>
        RE = 638,
        /// <summary>
        /// Saint Barthélemy
        /// </summary>
        BL = 652,
        /// <summary>
        /// Saint Helena, Ascension and Tristan da Cunha
        /// </summary>
        SH = 654,
        /// <summary>
        /// Saint Kitts and Nevis
        /// </summary>
        KN = 659,
        /// <summary>
        /// Saint Lucia
        /// </summary>
        LC = 662,
        /// <summary>
        /// Saint Martin (French part)
        /// </summary>
        MF = 663,
        /// <summary>
        /// Saint Pierre and Miquelon
        /// </summary>
        PM = 666,
        /// <summary>
        /// Saint Vincent and the Grenadines
        /// </summary>
        VC = 670,
        /// <summary>
        /// Samoa
        /// </summary>
        WS = 882,
        /// <summary>
        /// San Marino
        /// </summary>
        SM = 674,
        /// <summary>
        /// Sao Tome and Principe
        /// </summary>
        ST = 678,
        /// <summary>
        /// Saudi Arabia
        /// </summary>
        SA = 682,
        /// <summary>
        /// Senegal
        /// </summary>
        SN = 686,
        /// <summary>
        /// Serbia
        /// </summary>
        RS = 688,
        /// <summary>
        /// Seychelles
        /// </summary>
        SC = 690,
        /// <summary>
        /// Sierra Leone
        /// </summary>
        SL = 694,
        /// <summary>
        /// Singapore
        /// </summary>
        SG = 702,
        /// <summary>
        /// Sint Maarten (Dutch part)
        /// </summary>
        SX = 534,
        /// <summary>
        /// Slovakia
        /// </summary>
        SK = 703,
        /// <summary>
        /// Slovenia
        /// </summary>
        SI = 705,
        /// <summary>
        /// Solomon Islands
        /// </summary>
        SB = 090,
        /// <summary>
        /// Somalia
        /// </summary>
        SO = 706,
        /// <summary>
        /// South Africa
        /// </summary>
        ZA = 710,
        /// <summary>
        /// South Georgia and the South Sandwich Islands
        /// </summary>
        GS = 239,
        /// <summary>
        /// South Sudan
        /// </summary>
        SS = 728,
        /// <summary>
        /// Spain
        /// </summary>
        ES = 724,
        /// <summary>
        /// Sri Lanka
        /// </summary>
        LK = 144,
        /// <summary>
        /// Sudan
        /// </summary>
        SD = 729,
        /// <summary>
        /// Suriname
        /// </summary>
        SR = 740,
        /// <summary>
        /// Svalbard and Jan Mayen
        /// </summary>
        SJ = 744,
        /// <summary>
        /// Sweden
        /// </summary>
        SE = 752,
        /// <summary>
        /// Switzerland
        /// </summary>
        CH = 756,
        /// <summary>
        /// Syrian Arab Republic
        /// </summary>
        SY = 760,
        /// <summary>
        /// Taiwan (Province of China)
        /// </summary>
        TW = 158,
        /// <summary>
        /// Tajikistan
        /// </summary>
        TJ = 762,
        /// <summary>
        /// Tanzania, the United Republic of
        /// </summary>
        TZ = 834,
        /// <summary>
        /// Thailand
        /// </summary>
        TH = 764,
        /// <summary>
        /// Timor-Leste
        /// </summary>
        TL = 626,
        /// <summary>
        /// Togo
        /// </summary>
        TG = 768,
        /// <summary>
        /// Tokelau
        /// </summary>
        TK = 772,
        /// <summary>
        /// Tonga
        /// </summary>
        TO = 776,
        /// <summary>
        /// Trinidad and Tobago
        /// </summary>
        TT = 780,
        /// <summary>
        /// Tunisia
        /// </summary>
        TN = 788,
        /// <summary>
        /// Turkey
        /// </summary>
        TR = 792,
        /// <summary>
        /// Turkmenistan
        /// </summary>
        TM = 795,
        /// <summary>
        /// Turks and Caicos Islands
        /// </summary>
        TC = 796,
        /// <summary>
        /// Tuvalu
        /// </summary>
        TV = 798,
        /// <summary>
        /// Uganda
        /// </summary>
        UG = 800,
        /// <summary>
        /// Ukraine
        /// </summary>
        UA = 804,
        /// <summary>
        /// United Arab Emirates
        /// </summary>
        AE = 784,
        /// <summary>
        /// United Kingdom of Great Britain and Northern Ireland
        /// </summary>
        GB = 826,
        /// <summary>
        /// United States Minor Outlying Islands
        /// </summary>
        UM = 581,
        /// <summary>
        /// United States of America
        /// </summary>
        US = 840,
        /// <summary>
        /// Uruguay
        /// </summary>
        UY = 858,
        /// <summary>
        /// Uzbekistan
        /// </summary>
        UZ = 860,
        /// <summary>
        /// Vanuatu
        /// </summary>
        VU = 548,
        /// <summary>
        /// Venezuela (Bolivarian Republic of)
        /// </summary>
        VE = 862,
        /// <summary>
        /// Viet Nam
        /// </summary>
        VN = 704,
        /// <summary>
        /// Virgin Islands (British)
        /// </summary>
        VG = 092,
        /// <summary>
        /// Virgin Islands (U.S.)
        /// </summary>
        VI = 850,
        /// <summary>
        /// Wallis and Futuna
        /// </summary>
        WF = 876,
        /// <summary>
        /// Western Sahara
        /// </summary>
        EH = 732,
        /// <summary>
        /// Yemen
        /// </summary>
        YE = 887,
        /// <summary>
        /// Zambia
        /// </summary>
        ZM = 894,
        /// <summary>
        /// Zimbabwe
        /// </summary>
        ZW = 716,
        /// <summary>
        /// Åland Islands
        /// </summary>
        AX = 248,
    }

    /// <summary>
    /// Represents a three letter alpha ISO 3166 code. The numeric value of each entry also represents its numeric
    /// ISO 3166 code.
    /// https://www.iso.org/iso-3166-country-codes.html
    /// </summary>
    public enum ISO3166Alpha3
    {
        /// <summary>
        /// The ISO 3166 Alpha 3 code is unavailable.
        /// </summary>
        NONE,
        /// <summary>
        /// Afghanistan
        /// </summary>
        AFG = 004,
        /// <summary>
        /// Albania
        /// </summary>
        ALB = 008,
        /// <summary>
        /// Algeria
        /// </summary>
        DZA = 012,
        /// <summary>
        /// American Samoa
        /// </summary>
        ASM = 016,
        /// <summary>
        /// Andorra
        /// </summary>
        AND = 020,
        /// <summary>
        /// Angola
        /// </summary>
        AGO = 024,
        /// <summary>
        /// Anguilla
        /// </summary>
        AIA = 660,
        /// <summary>
        /// Antarctica
        /// </summary>
        ATA = 010,
        /// <summary>
        /// Antigua and Barbuda
        /// </summary>
        ATG = 028,
        /// <summary>
        /// Argentina
        /// </summary>
        ARG = 032,
        /// <summary>
        /// Armenia
        /// </summary>
        ARM = 051,
        /// <summary>
        /// Aruba
        /// </summary>
        ABW = 533,
        /// <summary>
        /// Australia
        /// </summary>
        AUS = 036,
        /// <summary>
        /// Austria
        /// </summary>
        AUT = 040,
        /// <summary>
        /// Azerbaijan
        /// </summary>
        AZE = 031,
        /// <summary>
        /// Bahamas
        /// </summary>
        BHS = 044,
        /// <summary>
        /// Bahrain
        /// </summary>
        BHR = 048,
        /// <summary>
        /// Bangladesh
        /// </summary>
        BGD = 050,
        /// <summary>
        /// Barbados
        /// </summary>
        BRB = 052,
        /// <summary>
        /// Belarus
        /// </summary>
        BLR = 112,
        /// <summary>
        /// Belgium
        /// </summary>
        BEL = 056,
        /// <summary>
        /// Belize
        /// </summary>
        BLZ = 084,
        /// <summary>
        /// Benin
        /// </summary>
        BEN = 204,
        /// <summary>
        /// Bermuda
        /// </summary>
        BMU = 060,
        /// <summary>
        /// Bhutan
        /// </summary>
        BTN = 064,
        /// <summary>
        /// Bolivia (Plurinational State of)
        /// </summary>
        BOL = 068,
        /// <summary>
        /// Bonaire, Sint Eustatius and Saba
        /// </summary>
        BES = 535,
        /// <summary>
        /// Bosnia and Herzegovina
        /// </summary>
        BIH = 070,
        /// <summary>
        /// Botswana
        /// </summary>
        BWA = 072,
        /// <summary>
        /// Bouvet Island
        /// </summary>
        BVT = 074,
        /// <summary>
        /// Brazil
        /// </summary>
        BRA = 076,
        /// <summary>
        /// British Indian Ocean Territory
        /// </summary>
        IOT = 086,
        /// <summary>
        /// Brunei Darussalam
        /// </summary>
        BRN = 096,
        /// <summary>
        /// Bulgaria
        /// </summary>
        BGR = 100,
        /// <summary>
        /// Burkina Faso
        /// </summary>
        BFA = 854,
        /// <summary>
        /// Burundi
        /// </summary>
        BDI = 108,
        /// <summary>
        /// Cabo Verde
        /// </summary>
        CPV = 132,
        /// <summary>
        /// Cambodia
        /// </summary>
        KHM = 116,
        /// <summary>
        /// Cameroon
        /// </summary>
        CMR = 120,
        /// <summary>
        /// Canada
        /// </summary>
        CAN = 124,
        /// <summary>
        /// Cayman Islands
        /// </summary>
        CYM = 136,
        /// <summary>
        /// Central African Republic
        /// </summary>
        CAF = 140,
        /// <summary>
        /// Chad
        /// </summary>
        TCD = 148,
        /// <summary>
        /// Chile
        /// </summary>
        CHL = 152,
        /// <summary>
        /// China
        /// </summary>
        CHN = 156,
        /// <summary>
        /// Christmas Island
        /// </summary>
        CXR = 162,
        /// <summary>
        /// Cocos (Keeling) Islands
        /// </summary>
        CCK = 166,
        /// <summary>
        /// Colombia
        /// </summary>
        COL = 170,
        /// <summary>
        /// Comoros
        /// </summary>
        COM = 174,
        /// <summary>
        /// Congo (the Democratic Republic of the)
        /// </summary>
        COD = 180,
        /// <summary>
        /// Congo
        /// </summary>
        COG = 178,
        /// <summary>
        /// Cook Islands
        /// </summary>
        COK = 184,
        /// <summary>
        /// Costa Rica
        /// </summary>
        CRI = 188,
        /// <summary>
        /// Croatia
        /// </summary>
        HRV = 191,
        /// <summary>
        /// Cuba
        /// </summary>
        CUB = 192,
        /// <summary>
        /// Curaçao
        /// </summary>
        CUW = 531,
        /// <summary>
        /// Cyprus
        /// </summary>
        CYP = 196,
        /// <summary>
        /// Czechia
        /// </summary>
        CZE = 203,
        /// <summary>
        /// Côte d'Ivoire
        /// </summary>
        CIV = 384,
        /// <summary>
        /// Denmark
        /// </summary>
        DNK = 208,
        /// <summary>
        /// Djibouti
        /// </summary>
        DJI = 262,
        /// <summary>
        /// Dominica
        /// </summary>
        DMA = 212,
        /// <summary>
        /// Dominican Republic
        /// </summary>
        DOM = 214,
        /// <summary>
        /// Ecuador
        /// </summary>
        ECU = 218,
        /// <summary>
        /// Egypt
        /// </summary>
        EGY = 818,
        /// <summary>
        /// El Salvador
        /// </summary>
        SLV = 222,
        /// <summary>
        /// Equatorial Guinea
        /// </summary>
        GNQ = 226,
        /// <summary>
        /// Eritrea
        /// </summary>
        ERI = 232,
        /// <summary>
        /// Estonia
        /// </summary>
        EST = 233,
        /// <summary>
        /// Eswatini
        /// </summary>
        SWZ = 748,
        /// <summary>
        /// Ethiopia
        /// </summary>
        ETH = 231,
        /// <summary>
        /// Falkland Islands [Malvinas]
        /// </summary>
        FLK = 238,
        /// <summary>
        /// Faroe Islands
        /// </summary>
        FRO = 234,
        /// <summary>
        /// Fiji
        /// </summary>
        FJI = 242,
        /// <summary>
        /// Finland
        /// </summary>
        FIN = 246,
        /// <summary>
        /// France
        /// </summary>
        FRA = 250,
        /// <summary>
        /// French Guiana
        /// </summary>
        GUF = 254,
        /// <summary>
        /// French Polynesia
        /// </summary>
        PYF = 258,
        /// <summary>
        /// French Southern Territories
        /// </summary>
        ATF = 260,
        /// <summary>
        /// Gabon
        /// </summary>
        GAB = 266,
        /// <summary>
        /// Gambia
        /// </summary>
        GMB = 270,
        /// <summary>
        /// Georgia
        /// </summary>
        GEO = 268,
        /// <summary>
        /// Germany
        /// </summary>
        DEU = 276,
        /// <summary>
        /// Ghana
        /// </summary>
        GHA = 288,
        /// <summary>
        /// Gibraltar
        /// </summary>
        GIB = 292,
        /// <summary>
        /// Greece
        /// </summary>
        GRC = 300,
        /// <summary>
        /// Greenland
        /// </summary>
        GRL = 304,
        /// <summary>
        /// Grenada
        /// </summary>
        GRD = 308,
        /// <summary>
        /// Guadeloupe
        /// </summary>
        GLP = 312,
        /// <summary>
        /// Guam
        /// </summary>
        GUM = 316,
        /// <summary>
        /// Guatemala
        /// </summary>
        GTM = 320,
        /// <summary>
        /// Guernsey
        /// </summary>
        GGY = 831,
        /// <summary>
        /// Guinea
        /// </summary>
        GIN = 324,
        /// <summary>
        /// Guinea-Bissau
        /// </summary>
        GNB = 624,
        /// <summary>
        /// Guyana
        /// </summary>
        GUY = 328,
        /// <summary>
        /// Haiti
        /// </summary>
        HTI = 332,
        /// <summary>
        /// Heard Island and McDonald Islands
        /// </summary>
        HMD = 334,
        /// <summary>
        /// Holy See
        /// </summary>
        VAT = 336,
        /// <summary>
        /// Honduras
        /// </summary>
        HND = 340,
        /// <summary>
        /// Hong Kong
        /// </summary>
        HKG = 344,
        /// <summary>
        /// Hungary
        /// </summary>
        HUN = 348,
        /// <summary>
        /// Iceland
        /// </summary>
        ISL = 352,
        /// <summary>
        /// India
        /// </summary>
        IND = 356,
        /// <summary>
        /// Indonesia
        /// </summary>
        IDN = 360,
        /// <summary>
        /// Iran (Islamic Republic of)
        /// </summary>
        IRN = 364,
        /// <summary>
        /// Iraq
        /// </summary>
        IRQ = 368,
        /// <summary>
        /// Ireland
        /// </summary>
        IRL = 372,
        /// <summary>
        /// Isle of Man
        /// </summary>
        IMN = 833,
        /// <summary>
        /// Israel
        /// </summary>
        ISR = 376,
        /// <summary>
        /// Italy
        /// </summary>
        ITA = 380,
        /// <summary>
        /// Jamaica
        /// </summary>
        JAM = 388,
        /// <summary>
        /// Japan
        /// </summary>
        JPN = 392,
        /// <summary>
        /// Jersey
        /// </summary>
        JEY = 832,
        /// <summary>
        /// Jordan
        /// </summary>
        JOR = 400,
        /// <summary>
        /// Kazakhstan
        /// </summary>
        KAZ = 398,
        /// <summary>
        /// Kenya
        /// </summary>
        KEN = 404,
        /// <summary>
        /// Kiribati
        /// </summary>
        KIR = 296,
        /// <summary>
        /// Korea (the Democratic People's Republic of)
        /// </summary>
        PRK = 408,
        /// <summary>
        /// Korea (the Republic of)
        /// </summary>
        KOR = 410,
        /// <summary>
        /// Kuwait
        /// </summary>
        KWT = 414,
        /// <summary>
        /// Kyrgyzstan
        /// </summary>
        KGZ = 417,
        /// <summary>
        /// Lao People's Democratic Republic
        /// </summary>
        LAO = 418,
        /// <summary>
        /// Latvia
        /// </summary>
        LVA = 428,
        /// <summary>
        /// Lebanon
        /// </summary>
        LBN = 422,
        /// <summary>
        /// Lesotho
        /// </summary>
        LSO = 426,
        /// <summary>
        /// Liberia
        /// </summary>
        LBR = 430,
        /// <summary>
        /// Libya
        /// </summary>
        LBY = 434,
        /// <summary>
        /// Liechtenstein
        /// </summary>
        LIE = 438,
        /// <summary>
        /// Lithuania
        /// </summary>
        LTU = 440,
        /// <summary>
        /// Luxembourg
        /// </summary>
        LUX = 442,
        /// <summary>
        /// Macao
        /// </summary>
        MAC = 446,
        /// <summary>
        /// Madagascar
        /// </summary>
        MDG = 450,
        /// <summary>
        /// Malawi
        /// </summary>
        MWI = 454,
        /// <summary>
        /// Malaysia
        /// </summary>
        MYS = 458,
        /// <summary>
        /// Maldives
        /// </summary>
        MDV = 462,
        /// <summary>
        /// Mali
        /// </summary>
        MLI = 466,
        /// <summary>
        /// Malta
        /// </summary>
        MLT = 470,
        /// <summary>
        /// Marshall Islands
        /// </summary>
        MHL = 584,
        /// <summary>
        /// Martinique
        /// </summary>
        MTQ = 474,
        /// <summary>
        /// Mauritania
        /// </summary>
        MRT = 478,
        /// <summary>
        /// Mauritius
        /// </summary>
        MUS = 480,
        /// <summary>
        /// Mayotte
        /// </summary>
        MYT = 175,
        /// <summary>
        /// Mexico
        /// </summary>
        MEX = 484,
        /// <summary>
        /// Micronesia (Federated States of)
        /// </summary>
        FSM = 583,
        /// <summary>
        /// Moldova (the Republic of)
        /// </summary>
        MDA = 498,
        /// <summary>
        /// Monaco
        /// </summary>
        MCO = 492,
        /// <summary>
        /// Mongolia
        /// </summary>
        MNG = 496,
        /// <summary>
        /// Montenegro
        /// </summary>
        MNE = 499,
        /// <summary>
        /// Montserrat
        /// </summary>
        MSR = 500,
        /// <summary>
        /// Morocco
        /// </summary>
        MAR = 504,
        /// <summary>
        /// Mozambique
        /// </summary>
        MOZ = 508,
        /// <summary>
        /// Myanmar
        /// </summary>
        MMR = 104,
        /// <summary>
        /// Namibia
        /// </summary>
        NAM = 516,
        /// <summary>
        /// Nauru
        /// </summary>
        NRU = 520,
        /// <summary>
        /// Nepal
        /// </summary>
        NPL = 524,
        /// <summary>
        /// Netherlands
        /// </summary>
        NLD = 528,
        /// <summary>
        /// New Caledonia
        /// </summary>
        NCL = 540,
        /// <summary>
        /// New Zealand
        /// </summary>
        NZL = 554,
        /// <summary>
        /// Nicaragua
        /// </summary>
        NIC = 558,
        /// <summary>
        /// Niger
        /// </summary>
        NER = 562,
        /// <summary>
        /// Nigeria
        /// </summary>
        NGA = 566,
        /// <summary>
        /// Niue
        /// </summary>
        NIU = 570,
        /// <summary>
        /// Norfolk Island
        /// </summary>
        NFK = 574,
        /// <summary>
        /// North Macedonia
        /// </summary>
        MKD = 807,
        /// <summary>
        /// Northern Mariana Islands
        /// </summary>
        MNP = 580,
        /// <summary>
        /// Norway
        /// </summary>
        NOR = 578,
        /// <summary>
        /// Oman
        /// </summary>
        OMN = 512,
        /// <summary>
        /// Pakistan
        /// </summary>
        PAK = 586,
        /// <summary>
        /// Palau
        /// </summary>
        PLW = 585,
        /// <summary>
        /// Palestine, State of
        /// </summary>
        PSE = 275,
        /// <summary>
        /// Panama
        /// </summary>
        PAN = 591,
        /// <summary>
        /// Papua New Guinea
        /// </summary>
        PNG = 598,
        /// <summary>
        /// Paraguay
        /// </summary>
        PRY = 600,
        /// <summary>
        /// Peru
        /// </summary>
        PER = 604,
        /// <summary>
        /// Philippines
        /// </summary>
        PHL = 608,
        /// <summary>
        /// Pitcairn
        /// </summary>
        PCN = 612,
        /// <summary>
        /// Poland
        /// </summary>
        POL = 616,
        /// <summary>
        /// Portugal
        /// </summary>
        PRT = 620,
        /// <summary>
        /// Puerto Rico
        /// </summary>
        PRI = 630,
        /// <summary>
        /// Qatar
        /// </summary>
        QAT = 634,
        /// <summary>
        /// Romania
        /// </summary>
        ROU = 642,
        /// <summary>
        /// Russian Federation
        /// </summary>
        RUS = 643,
        /// <summary>
        /// Rwanda
        /// </summary>
        RWA = 646,
        /// <summary>
        /// Réunion
        /// </summary>
        REU = 638,
        /// <summary>
        /// Saint Barthélemy
        /// </summary>
        BLM = 652,
        /// <summary>
        /// Saint Helena, Ascension and Tristan da Cunha
        /// </summary>
        SHN = 654,
        /// <summary>
        /// Saint Kitts and Nevis
        /// </summary>
        KNA = 659,
        /// <summary>
        /// Saint Lucia
        /// </summary>
        LCA = 662,
        /// <summary>
        /// Saint Martin (French part)
        /// </summary>
        MAF = 663,
        /// <summary>
        /// Saint Pierre and Miquelon
        /// </summary>
        SPM = 666,
        /// <summary>
        /// Saint Vincent and the Grenadines
        /// </summary>
        VCT = 670,
        /// <summary>
        /// Samoa
        /// </summary>
        WSM = 882,
        /// <summary>
        /// San Marino
        /// </summary>
        SMR = 674,
        /// <summary>
        /// Sao Tome and Principe
        /// </summary>
        STP = 678,
        /// <summary>
        /// Saudi Arabia
        /// </summary>
        SAU = 682,
        /// <summary>
        /// Senegal
        /// </summary>
        SEN = 686,
        /// <summary>
        /// Serbia
        /// </summary>
        SRB = 688,
        /// <summary>
        /// Seychelles
        /// </summary>
        SYC = 690,
        /// <summary>
        /// Sierra Leone
        /// </summary>
        SLE = 694,
        /// <summary>
        /// Singapore
        /// </summary>
        SGP = 702,
        /// <summary>
        /// Sint Maarten (Dutch part)
        /// </summary>
        SXM = 534,
        /// <summary>
        /// Slovakia
        /// </summary>
        SVK = 703,
        /// <summary>
        /// Slovenia
        /// </summary>
        SVN = 705,
        /// <summary>
        /// Solomon Islands
        /// </summary>
        SLB = 090,
        /// <summary>
        /// Somalia
        /// </summary>
        SOM = 706,
        /// <summary>
        /// South Africa
        /// </summary>
        ZAF = 710,
        /// <summary>
        /// South Georgia and the South Sandwich Islands
        /// </summary>
        SGS = 239,
        /// <summary>
        /// South Sudan
        /// </summary>
        SSD = 728,
        /// <summary>
        /// Spain
        /// </summary>
        ESP = 724,
        /// <summary>
        /// Sri Lanka
        /// </summary>
        LKA = 144,
        /// <summary>
        /// Sudan
        /// </summary>
        SDN = 729,
        /// <summary>
        /// Suriname
        /// </summary>
        SUR = 740,
        /// <summary>
        /// Svalbard and Jan Mayen
        /// </summary>
        SJM = 744,
        /// <summary>
        /// Sweden
        /// </summary>
        SWE = 752,
        /// <summary>
        /// Switzerland
        /// </summary>
        CHE = 756,
        /// <summary>
        /// Syrian Arab Republic
        /// </summary>
        SYR = 760,
        /// <summary>
        /// Taiwan (Province of China)
        /// </summary>
        TWN = 158,
        /// <summary>
        /// Tajikistan
        /// </summary>
        TJK = 762,
        /// <summary>
        /// Tanzania, the United Republic of
        /// </summary>
        TZA = 834,
        /// <summary>
        /// Thailand
        /// </summary>
        THA = 764,
        /// <summary>
        /// Timor-Leste
        /// </summary>
        TLS = 626,
        /// <summary>
        /// Togo
        /// </summary>
        TGO = 768,
        /// <summary>
        /// Tokelau
        /// </summary>
        TKL = 772,
        /// <summary>
        /// Tonga
        /// </summary>
        TON = 776,
        /// <summary>
        /// Trinidad and Tobago
        /// </summary>
        TTO = 780,
        /// <summary>
        /// Tunisia
        /// </summary>
        TUN = 788,
        /// <summary>
        /// Turkey
        /// </summary>
        TUR = 792,
        /// <summary>
        /// Turkmenistan
        /// </summary>
        TKM = 795,
        /// <summary>
        /// Turks and Caicos Islands
        /// </summary>
        TCA = 796,
        /// <summary>
        /// Tuvalu
        /// </summary>
        TUV = 798,
        /// <summary>
        /// Uganda
        /// </summary>
        UGA = 800,
        /// <summary>
        /// Ukraine
        /// </summary>
        UKR = 804,
        /// <summary>
        /// United Arab Emirates
        /// </summary>
        ARE = 784,
        /// <summary>
        /// United Kingdom of Great Britain and Northern Ireland
        /// </summary>
        GBR = 826,
        /// <summary>
        /// United States Minor Outlying Islands
        /// </summary>
        UMI = 581,
        /// <summary>
        /// United States of America
        /// </summary>
        USA = 840,
        /// <summary>
        /// Uruguay
        /// </summary>
        URY = 858,
        /// <summary>
        /// Uzbekistan
        /// </summary>
        UZB = 860,
        /// <summary>
        /// Vanuatu
        /// </summary>
        VUT = 548,
        /// <summary>
        /// Venezuela (Bolivarian Republic of)
        /// </summary>
        VEN = 862,
        /// <summary>
        /// Viet Nam
        /// </summary>
        VNM = 704,
        /// <summary>
        /// Virgin Islands (British)
        /// </summary>
        VGB = 092,
        /// <summary>
        /// Virgin Islands (U.S.)
        /// </summary>
        VIR = 850,
        /// <summary>
        /// Wallis and Futuna
        /// </summary>
        WLF = 876,
        /// <summary>
        /// Western Sahara
        /// </summary>
        ESH = 732,
        /// <summary>
        /// Yemen
        /// </summary>
        YEM = 887,
        /// <summary>
        /// Zambia
        /// </summary>
        ZMB = 894,
        /// <summary>
        /// Zimbabwe
        /// </summary>
        ZWE = 716,
        /// <summary>
        /// Åland Islands
        /// </summary>
        ALA = 248,
    }
}