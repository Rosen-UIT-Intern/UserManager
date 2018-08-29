using System.Collections.Generic;

using UserManager.Contract.DTOs;

namespace UserManager.Contract
{
    public interface ISearchService
    {
        /// <summary>
        /// search for user that fit the querry <para/>
        /// //todo implement some kind of general search querry
        /// </summary>
        /// <param name="querry">the querry</param>
        /// <returns>a list of user that fit the querry</returns>
        IEnumerable<UserDTO> Search(QuerryDTO querry);
    }
}
