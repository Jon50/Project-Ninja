using System;
using System.Collections.Generic;
using TongueTwister.Extensions;
using UnityEngine;

namespace TongueTwister.Models
{
    /// <summary>
    /// Simple container class for holding a list of models.
    /// </summary>
    [Serializable]
    public class TongueTwisterModelCollection
    {
        /// <summary>
        /// The list of models contained within this collection.
        /// </summary>
        [SerializeField] public List<TongueTwisterModel> Models = new List<TongueTwisterModel>();

        /// <summary>
        /// Creates a new model collection.
        /// </summary>
        public TongueTwisterModelCollection()
        {
            
        }
        
        /// <summary>
        /// Creates a new model collection by copying a list of given models, not retaining the original object
        /// reference for each one.
        /// </summary>
        /// <param name="copyModels">The models to create a copy from.</param>
        public TongueTwisterModelCollection(List<TongueTwisterModel> copyModels)
        {
            Models = copyModels.Copy();
        }

        /// <summary>
        /// Creates a new model collection by copying an existing one and individually copying the listed ones. 
        /// </summary>
        /// <param name="copy">The model collection to create a copy from.</param>
        public TongueTwisterModelCollection(TongueTwisterModelCollection copy)
        {
            Models = copy.Models.Copy();
        }
    }
}