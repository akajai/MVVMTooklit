using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM.DBContext;
using MVVMLight.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;

namespace MVVM.ModelView
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;
        public MainWindowViewModel()
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            _dbContext = new AppDbContext(connectionString);
            LoadFromDb();
        }
        private void LoadFromDb()
        {
            var fileNames = _dbContext.Table<FileName>().ToList();
            allFileNames = new ObservableCollection<FileName>(fileNames);
            OnPropertyChanged("allFileNames");
        }
        [ObservableProperty]
        public FileName selectedItem = new FileName();
        [ObservableProperty]
        public ObservableCollection<FileName> allFileNames = new ObservableCollection<FileName>();
        [RelayCommand]
        public void Save()
        {
            List<int> validIds = allFileNames.Select(x => x.Id).ToList();
            List<int> deleteIds = _dbContext.Table<FileName>().Select(f => f.Id).Except(validIds).ToList();
            List<int> updateFileIds = _dbContext.Table<FileName>().Where(f => validIds.Contains(f.Id)).Select(f => f.Id).ToList();
            List<FileName> updateFileNames = allFileNames.Where(f => updateFileIds.Contains(f.Id)).ToList();
            List<int> newFileIds = validIds.Except(deleteIds).ToList().Except(updateFileNames.Select(u => u.Id).ToList()).ToList();
            List<FileName> newFileNames = allFileNames.Where(f => newFileIds.Contains(f.Id)).ToList();
            if (deleteIds.Any())
            {
                var delFiles = _dbContext.Table<FileName>().Where(x => deleteIds.Contains(x.Id)).ToList();
                delFiles.ForEach(x =>
                {
                    _dbContext.Delete<FileName>(delFiles);
                });
            }
            if (updateFileNames.Any())
            {
                _dbContext.UpdateAll(updateFileNames);
            }
            if (newFileNames.Any())
            {
                _dbContext.InsertAll(newFileNames);
            }
            LoadFromDb();
        }
        [RelayCommand]
        public void Cancel()
        {
            LoadFromDb();
        }
    }

}
