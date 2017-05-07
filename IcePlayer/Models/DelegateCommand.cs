using System;
using System.Windows.Input;

namespace IcePlayer.Models
{
    /// <summary>  
    /// 実行する処理と、実行可能かどうかの判断を  
    /// delegateで指定可能なコマンドクラス。  
    /// </summary>  
    public class DelegateCommand : ICommand
    {
        private Action<object>     _executeAction;
        private Func<object, bool> _canExecuteAction;

        public DelegateCommand( Action<object> executeAction, Func<object, bool> canExecuteAction )
        {
            _executeAction    = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        #region ICommand メンバ  

        public bool CanExecute( object parameter )
        {
            return _canExecuteAction( parameter );
        }

        // CommandManagerからイベント発行してもらうようにする  
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute( object parameter )
        {
            _executeAction( parameter );
            return;
        }

        #endregion
    }
}