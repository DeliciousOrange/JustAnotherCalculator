using Calculator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using JustAnotherCalculator.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace JustAnotherCalculator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            OperationList = new ObservableCollection<Operation>();
            
            if (!this.IsInDesignMode)
            {
                DoOpenExecute();
            }
        }

        #region Property UserInput
        private String _userInput;
        public String UserInput
        {
            get
            {
                return _userInput;
            }
            set
            {
                _userInput = value;
                RaisePropertyChanged("UserInput");
            }
        }
        #endregion

        #region ICommand DoEvaluateUserInput
        private ICommand _doEvaluateUserInput;
        public ICommand DoEvaluateUserInput
        {
            get
            {
                if (_doEvaluateUserInput == null)
                {
                    _doEvaluateUserInput = new RelayCommand(DoEvaluateUserInputExecute); // 
                }
                return _doEvaluateUserInput;
            }
        }
        private void DoEvaluateUserInputExecute()
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {
                CalculatorOutput = CalculatorHelper.GetInstance().Evaluate(UserInput);

                OperationList.Insert(0, new Operation()
                {
                    Input = UserInput,
                    Result = CalculatorOutput
                });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Exception found on DoEvaluateUserInputExecute :" + ex.Message);
            }
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
        #endregion

        #region Property CalculatorOutput
        private String _calculatorOutput;
        public String CalculatorOutput
        {
            get
            {
                return _calculatorOutput;
            }
            set
            {
                _calculatorOutput = value;
                RaisePropertyChanged("CalculatorOutput");
            }
        }
        #endregion

        #region OperationList
        private ObservableCollection<Operation> _operationList;
        public ObservableCollection<Operation> OperationList
        {
            get
            {
                return _operationList;
            }
            set
            {
                _operationList = value;
                RaisePropertyChanged("OperationList");
            }
        }
        #endregion

        public Operation SelectedOperation { get; set; }

        #region ICommand DoAddResult
        private ICommand _doAddResult;
        public ICommand DoAddResult
        {
            get
            {
                if (_doAddResult == null)
                {
                    _doAddResult = new RelayCommand(DoAddResultExecute); // 
                }
                return _doAddResult;
            }
        }
        private void DoAddResultExecute()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {

                if (SelectedOperation != null)
                {
                    UserInput += " " + SelectedOperation.Result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found on DoAddResultExecute :" + ex.Message);
            }
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
        #endregion


        #region ICommand DoDeleteOperation
        private ICommand _doDeleteOperation;
        public ICommand DoDeleteOperation
        {
            get
            {
                if (_doDeleteOperation == null)
                {
                    _doDeleteOperation = new RelayCommand(DoDeleteOperationExecute); // RelayCommand
                }
                return _doDeleteOperation;
            }
        }
        private void DoDeleteOperationExecute()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {
                if (SelectedOperation != null && OperationList != null && OperationList.Contains(SelectedOperation))
                {
                    OperationList.Remove(SelectedOperation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found on DoDeleteOperationExecute :" + ex.Message);
            }
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
        #endregion

        #region ICommand DoSave
        private ICommand _doSave;
        public ICommand DoSave
        {
            get
            {
                if (_doSave == null)
                {
                    _doSave = new RelayCommand(DoSaveExecute); // 
                }
                return _doSave;
            }
        }
        private void DoSaveExecute()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {
                if (OperationList != null)
                {
                    var curentState = Newtonsoft.Json.JsonConvert.SerializeObject(OperationList, Formatting.Indented);

                    File.WriteAllText("list.json", curentState, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found on DoSaveExecute :" + ex.Message);
            }
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
        #endregion


        #region ICommand DoOpen
        private ICommand _doOpen;
        public ICommand DoOpen
        {
            get
            {
                if (_doOpen == null)
                {
                    _doOpen = new RelayCommand(DoOpenExecute); // RelayCommand
                }
                return _doOpen;
            }
        }
        private void DoOpenExecute()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {
                string jsonContent = File.ReadAllText("list.json");
                OperationList = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Operation>>(jsonContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found on DoOpenExecute :" + ex.Message);
            }
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
        #endregion























    }
}