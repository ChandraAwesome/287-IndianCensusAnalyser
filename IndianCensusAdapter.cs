using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indian_State_Census_Analyser
{
    class IndianCensusAdapter : CensusAdapter
    {
        string[] censusData;
        Dictionary<string, DTO.CensusDTO> dataMap;
        public Dictionary<string, DTO.CensusDTO> LoadCensusData(string csvFilePath, string dataHeaders)
        {
            dataMap = new Dictionary<string, DTO.CensusDTO>();
            censusData = GetCensusData(csvFilePath, dataHeaders);
            foreach (string data in censusData.Skip(1))
            {
                if (!data.Contains(","))
                {
                    throw new CensusAnalyserException("File contains wrong delimiter", CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER);

                }
                string[] column = data.Split(",");
                if (csvFilePath.Contains("IndiaStateCode.csv"))
                    dataMap.Add(column[1], new DTO.CensusDTO(new POCO.StateCodeDAO(column[0], column[1], column[2], column[3])));
                if (csvFilePath.Contains("IndiaStateCensusData.csv"))
                    dataMap.Add(column[0], new DTO.CensusDTO(new POCO.CensusDataDAO(column[0], column[1], column[2], column[3])));

            }

            return dataMap.ToDictionary(p => p.Key, p => p.Value);
        }
    }

}
