//import { loadStripe } from "@stripe/stripe-js";

//const stripePromise = loadStripe("pk_test_your_public_key"); // ?? your Stripe public key

//export default function CheckoutButton({ itemId }) {
//    const handleCheckout = async () => {
//        try {
//            const stripe = await stripePromise;

//            // Call your .NET API
//            const res = await fetch("/api/payment/create-session", {
//                method: "POST",
//                headers: {
//                    "Content-Type": "application/json"
//                },
//                body: JSON.stringify({ itemId })
//            });

//            const data = await res.json();

//            // Redirect to Stripe Checkout
//            const result = await stripe.redirectToCheckout({
//                sessionId: data.sessionId
//            });

//            if (result.error) {
//                alert(result.error.message);
//            }
//        } catch (err) {
//            console.error(err);
//            alert("Checkout failed");
//        }
//    };

//    return (
//        <button onClick={handleCheckout}>
//            Pay Now
//        </button>
//    );
//}