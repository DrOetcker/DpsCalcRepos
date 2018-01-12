namespace Oetcker.Gui.Interfaces
{
    public interface IResetable
    {
        #region Methods

        void Reset();

        #endregion
    }

    public interface IResetable<in T>
    {
        #region Methods

        void Reset(T obj);

        #endregion
    }
}
