using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Oetcker.Gui.Interfaces;
using Prism.Mvvm;

namespace Oetcker.Gui
{
    /// <summary>
    /// Basisklasse für das ViewModel
    /// </summary>
    public class ViewModelBase : BindableBase, IViewModelBase
    {
        #region Properties

        /// <summary>
        /// Diese Property definiert einen Status der angibt ob die Funktion <see cref="Dispose()"/> bereits 
        /// aufgrufen wurde und somit dem <see cref="GC"/> zur Vernichtung übergeben wurde.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool IsDisposed { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Diese Methode führt anwendungsspezifische Aufgaben durch, die mit der Freigabe, der Zurückgabe oder 
        /// dem Zurücksetzen von nicht verwalteten Ressourcen zusammenhängen.
        /// </summary>
        /// <author>Seiler</author>
        public virtual void Dispose()
        {
            IsDisposed = true;
        }

        /// <summary>
        /// Diese Methode löst den Event <see cref="BindableBase.PropertyChanged"/>-Event aus. Hierdurch wird 
        /// die GUI angehalten sich bzw. das entsprechenden Binding zu aktualsieren.
        /// 
        /// Mit der umstellung auf Prism 5 ist das Prism-Objekt "NotificationObject" auf obsolete gestellt. 
        /// Aus diesem Grund und die Verringerung des Umbauaufwandes wurde diese Methode kopiert.
        /// </summary>
        /// <typeparam name="T">Type der Eigenschaft, die einen neuen Wert hat</typeparam>
        /// <param name="propertyExpression">Ein Lambda-Ausdruck, der die Eigenschaft darstellt, die einen neuen Wert hat.</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            OnPropertyChanged(PropertySupport.ExtractPropertyName(propertyExpression));
        }

        /// <summary>
        /// Diese Methode wird aufgerufen um einen Bereich eines Applikationsmodule zu aktualisieren und anzuzeigen.
        /// </summary>
        public virtual void Refresh()
        {
            if (IsDisposed)
                throw new ObjectDisposedException($"The instance of the class '{GetType()}' is already disposed.");
        }

        /// <summary>
        /// Diese Methode setzt die Instanz des ViewModel auf seinen Ausgangszustand zurück.
        /// </summary>
        public virtual void Reset()
        { }

        #endregion
    }
}
