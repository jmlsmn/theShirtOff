using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Repositories;
using PetaPoco;
using DomainModel.Abstract.Entities;

namespace DomainModel.Concrete.Repositories
{
    class ImageRepository : IImageRepository
    {
        #region Private Members

        private string _connectionStringName;
        private Database _db;

        #endregion

        #region Constructor

        public ImageRepository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
            InitializeDatabase();
        }

        #endregion

        #region Private Methods

        private void InitializeDatabase()
        {
            _db = new Database(_connectionStringName);
            _db.EnableAutoSelect = false;
            _db.EnableNamedParams = false;
            _db.ForceDateTimesToUtc = false;
        }

        #endregion

        #region IImageRepository Methods

        public void AddImage(IImage image)
        {
            _db.Insert(image);
        }

        #endregion
    }
}
