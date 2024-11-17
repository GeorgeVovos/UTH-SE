import java.util.Date;
import java.util.ArrayList;
import java.util.List;
import java.util.Collections;

//Sure, an overkill for this example but we like functional programming so, we use immutable objects
public class OrderDemo {
    public static void main(String[] args) {
        Product pizzaProduct = new Product("Pizza", 15);
        Product pitaProduct = new Product("Pita", 3);

        OrderLine orderLine1 = new OrderLine(pizzaProduct, 2);
        OrderLine orderLine2 = new OrderLine(pitaProduct, 4);

        Order order = new Order(new Date(), false)
                         .addOrderLine(orderLine1)
                         .addOrderLine(orderLine2);

        // Made a mistake
        order = order.removeOrderLine(orderLine2)
                     .addOrderLine(new OrderLine(pitaProduct, 5));

        order.printOrderDetails();

        order.dispatch();
        order.close();
    }
}

class Product {
    final private String name;
    final private double unitPrice;

    public Product(String name, double unitPrice) {
        if (name == null || name.trim().length() == 0 || unitPrice <= 0)
            throw new IllegalArgumentException("Invalid product details");
        this.name = name;
        this.unitPrice = unitPrice;
    }

    public String getName() {
        return name;
    }

    public double getUnitPrice() {
        return unitPrice;
    }
}

class OrderLine {
    final private int quantity;
    final private Product product;

    public OrderLine(Product product, int quantity) {
        if (product == null || quantity < 1)
            throw new IllegalArgumentException("Invalid OrderLine details");

        this.product = product;
        this.quantity = quantity;
    }

    public int getQuantity() {
        return quantity;
    }

    public Product getProduct() {
        return product;
    }

    public double getLineItemTotal() {
        return product.getUnitPrice() * quantity;
    }
}

class Order {
    private final Date date;
    private final boolean isPrepaid;
    private final List<OrderLine> orderLines;

    public Order(Date date, boolean isPrepaid) {
        this(date, isPrepaid, new ArrayList<>());
    }

    private Order(Date date, boolean isPrepaid, List<OrderLine> orderLines) {
        this.date = new Date(date.getTime());
        this.isPrepaid = isPrepaid;
        this.orderLines = Collections.unmodifiableList(new ArrayList<>(orderLines));
    }

    public Order addOrderLine(OrderLine orderLine) {
        List<OrderLine> newOrderLines = new ArrayList<>(this.orderLines);
        newOrderLines.add(orderLine);
        return new Order(this.date, this.isPrepaid, newOrderLines);
    }

    public Order removeOrderLine(OrderLine orderLine) {
        List<OrderLine> newOrderLines = new ArrayList<>(this.orderLines);
        newOrderLines.remove(orderLine);
        return new Order(this.date, this.isPrepaid, newOrderLines);
    }

    private double getTotal() {
        return orderLines.stream().mapToDouble(OrderLine::getLineItemTotal).sum();
    }

    public void printOrderDetails() {
        System.out.println("Order Details:");
        System.out.println("\tDate: " + date);
        System.out.println("\tIs Prepaid: " + isPrepaid);
        System.out.println("\tOrder Lines:");
        for (OrderLine line : orderLines) {
            System.out.println("\t\t-Product: " + line.getProduct().getName());
            System.out.println("\t\t   Quantity: " + line.getQuantity());
            System.out.println("\t\t   Line Price: " + line.getLineItemTotal());
        }
        System.out.println("\tTotal: " + getTotal());
    }

    public void dispatch() {
        System.out.println("Order dispatched.");
    }

    public void close() {
        System.out.println("Order closed.");
    }
}
