namespace ElectricalShop.Models
{
    public class AppRepository
    {
        //fields
        CategoryRepository categoryRepo;
        ProductRepository productRepo;
        MemberRepository memberRepo;
        CartRepository cartRepo;
        RoleRepository roleRepo;
        MemberInRoleRepository memberInRoleRepo;
        InvoiceRepository invoiceRepo;
        InvoiceDetailRepository invoiceDetailRepo;
        PaymentRepository paymentRepo;


        public PaymentRepository PaymentRepo
        {
            get
            {
                if (paymentRepo is null)
                {
                    paymentRepo = new PaymentRepository();
                }
                return paymentRepo;
            }
        }

        public InvoiceDetailRepository InvoiceDetailRepo
        {
            get
            {
                if (invoiceDetailRepo is null)
                {
                    invoiceDetailRepo = new InvoiceDetailRepository();
                }
                return invoiceDetailRepo;
            }
        }

        public InvoiceRepository InvoiceRepo
        {
            get
            {
                if (invoiceRepo is null)
                {
                    invoiceRepo = new InvoiceRepository();
                }
                return invoiceRepo;
            }
        }

        public MemberInRoleRepository MemberInRoleRepo
        {
            get
            {
                if (memberInRoleRepo is null)
                {
                    memberInRoleRepo = new MemberInRoleRepository();
                }
                return memberInRoleRepo;
            }
        }

        public RoleRepository RoleRepo
        {
            get
            {
                if (roleRepo is null)
                {
                    roleRepo = new RoleRepository();
                }
                return roleRepo;
            }
        }

        public CartRepository CartRepo
        {
            get
            {
                if (cartRepo is null)
                {
                    cartRepo = new CartRepository();
                }
                return cartRepo;
            }
        }

        public MemberRepository MemberRepo
        {
            get
            {
                if (memberRepo is null)
                {
                    memberRepo = new MemberRepository();
                }
                return memberRepo;
            }
        }

        public CategoryRepository CategoryRepo
        {
            get
            {
                if (categoryRepo is null)
                {
                    categoryRepo = new CategoryRepository();
                }
                return categoryRepo;
            }
        }

        public ProductRepository ProductRepo
        {
            get
            {
                if (productRepo is null)
                {
                    productRepo = new ProductRepository();
                }
                return productRepo;
            }
        }
    }
}
