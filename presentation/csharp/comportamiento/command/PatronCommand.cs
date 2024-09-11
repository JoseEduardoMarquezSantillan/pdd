using System;

// Interfaz del comando.
public interface ICommand
{
    void Execute();
}

// Comando para registrar una venta simple.
class RegisterSaleCommand : ICommand
{
    private string _product;
    private int _quantity;

    public RegisterSaleCommand(string product, int quantity)
    {
        this._product = product;
        this._quantity = quantity;
    }

    public void Execute()
    {
        Console.WriteLine("Registrando venta: Producto: " + _product + ", Cantidad: " + _quantity);
    }
}

// Comando para aplicar un descuento a la venta.
class ApplyDiscountCommand : ICommand
{
    private double _discountPercentage;

    public ApplyDiscountCommand(double discountPercentage)
    {
        this._discountPercentage = discountPercentage;
    }

    public void Execute()
    {
        Console.WriteLine("Aplicando descuento del " + _discountPercentage + "% a la venta.");
    }
}

// Comando complejo para actualizar el inventario.
class UpdateInventoryCommand : ICommand
{
    private InventoryManager _inventoryManager;
    private string _product;
    private int _quantity;

    public UpdateInventoryCommand(InventoryManager inventoryManager, string product, int quantity)
    {
        this._inventoryManager = inventoryManager;
        this._product = product;
        this._quantity = quantity;
    }

    public void Execute()
    {
        Console.WriteLine("Actualizando inventario...");
        _inventoryManager.RemoveProduct(_product, _quantity);
    }
}

// Receptor que maneja el inventario.
class InventoryManager
{
    public void RemoveProduct(string product, int quantity)
    {
        Console.WriteLine("Inventario: Producto '" + product + "' reducido en " + quantity + " unidades.");
    }

    public void AddProduct(string product, int quantity)
    {
        Console.WriteLine("Inventario: Producto '" + product + "' incrementado en " + quantity + " unidades.");
    }
}

// El Invocador que manejará los comandos.
class PointOfSaleInvoker
{
    private ICommand _onSale;
    private ICommand _onDiscount;
    private ICommand _onInventoryUpdate;

    public void SetSaleCommand(ICommand command)
    {
        this._onSale = command;
    }

    public void SetDiscountCommand(ICommand command)
    {
        this._onDiscount = command;
    }

    public void SetInventoryUpdateCommand(ICommand command)
    {
        this._onInventoryUpdate = command;
    }

    public void ProcessSale()
    {
        Console.WriteLine("Iniciando proceso de venta...");

        if (this._onSale is ICommand)
        {
            this._onSale.Execute();
        }

        if (this._onDiscount is ICommand)
        {
            this._onDiscount.Execute();
        }

        if (this._onInventoryUpdate is ICommand)
        {
            this._onInventoryUpdate.Execute();
        }

        Console.WriteLine("Proceso de venta finalizado.");
    }
}

// Cliente
class Program
{
    static void Main(string[] args)
    {
        // Crear el invocador (punto de venta)
        PointOfSaleInvoker pos = new PointOfSaleInvoker();

        // Configurar los comandos
        pos.SetSaleCommand(new RegisterSaleCommand("Laptop", 1));
        pos.SetDiscountCommand(new ApplyDiscountCommand(10)); // 10% de descuento
        InventoryManager inventoryManager = new InventoryManager();
        pos.SetInventoryUpdateCommand(new UpdateInventoryCommand(inventoryManager, "Laptop", 1));

        // Ejecutar el proceso de venta
        pos.ProcessSale();
    }
}
