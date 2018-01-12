namespace Oetcker.Gui.Interfaces
{
    public interface IRefreshable
    {
        #region Methods

        void Refresh();

        #endregion
    }

    public interface IRefreshable<in T>
    {
        #region Methods

        void Refresh(T obj);

        #endregion
    }
}