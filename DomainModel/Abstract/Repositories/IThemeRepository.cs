using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;

namespace DomainModel.Abstract.Repositories
{
    public interface IThemeRepository
    {
        IQueryable<ITheme> Themes { get; }
        void AddTheme(ITheme theme);
        void DeleteTheme(int themeID);
    }
}
