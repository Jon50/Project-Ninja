using System;
using System.Collections.Generic;
using System.Linq;

namespace TongueTwister.Editor.Trees.LocaleEditorTree
{
    public class LocaleEditorTree : Locale
    {
        #region Private

        private List<Locale> _data;

        #endregion

        #region Properties

        public event Action OnModelChanged;

        public List<Locale> Data => _data;

        public LocaleEditorTreeViewItem Root => new LocaleEditorTreeViewItem(-1, -1, "root", null);

        #endregion

        #region Constructors

        private void Initialize(List<Locale> data)
        {
            if (data != _data)
            {
                _data?.Clear();
                _data = data ?? new List<Locale>();
            }

            OnModelChanged?.Invoke();
        }

        public LocaleEditorTree(List<Locale> data) => Initialize(data);

        #endregion

        #region Utilities

        public Locale Find(string guid) => _data.FirstOrDefault(locale => locale.Id == guid);

        public void SetDataFromList(List<Locale> data) => Initialize(data);

        public void Remove(Locale locale)
        {
            _data.Remove(locale);
            MarkAsChanged();
        }

        public void Duplicate(Locale locale)
        {
            var clone = new Locale(locale, true);
            Add(clone);
        }

        public void Add(Locale locale)
        {
            _data.Add(locale);
            MarkAsChanged();
        }

        public void MarkAsChanged()
        {
            OnModelChanged?.Invoke();
        }

        #endregion
    }
}