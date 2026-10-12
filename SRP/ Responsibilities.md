1-WardBoard

Bed and Patient Management: Assigns patients to beds and stores patient identifiers and acuity scores.

Clinical Acuity Scoring: Calculates patient acuity scores based on heart rate and oxygen saturation.

Pager Alert Policy: Determines when to trigger a pager alert based on acuity thresholds and records alert messages.

Nurse Handoff Note Formatting: Generates formatted handoff notes containing bed patient acuity and status information.

Pager Log Management: Stores pager messages and returns them while clearing the internal log.

Census CSV Export: Exports bed assignments patient identifiers and acuity scores as CSV data.

2-AppointmentDesk

Business Hours Rules: Determines whether an appointment falls within the clinic's operating hours and allowed working days.

Appointment Slot Search: Aligns timestamps to slot boundaries and searches for the next available appointment slot.

Appointment Booking Management: Validates appointment slots and tracks booked appointments to prevent duplicate bookings.

ICS Calendar Generation: Generates calendar event data in ICS format for interoperability with calendar applications.

SMS Reminder Formatting: Generates SMS reminder messages containing appointment details and clinic contact information.

3-CheckoutBasket

Cart Item Management: Stores cart items, including their SKUs, prices, and quantities, and validates item quantities when adding lines.

Pricing and Discount Calculation: Calculates the subtotal, applies discounts, includes additional charges, and determines the final total.

Coupon Parsing: Parses coupon text and interprets supported coupon formats and discount rules.

Gift-Wrap Fee Policy: Applies the gift-wrapping fee when gift wrapping is enabled.

Gift Message Formatting: Generates a customer-facing gift message containing the sender's name, item information, and total value.

Payment Authorization Stub: Generates a simulated payment authorization code using cart and payment-related data.

4-CourseEnrollmentDesk

Enrollment and Capacity Management: Registers students, assigns available seats, and determines whether new registrations can be accepted.

Duplicate Registration Validation: Prevents students from registering more than once by checking existing enrollments and waitlist entries.

Waitlist Management: Tracks students waiting for available seats, determines their positions, and promotes students when seats become available.

Welcome Packet Formatting: Generates a Markdown welcome packet containing course information, student status, and onboarding instructions.

Tuition Invoice Generation: Generates formatted tuition invoice lines for enrolled students and waitlisted students.

Tuition and Tax Calculation: Calculates tuition totals and VAT amounts for enrolled students.

5-GradeBook

Score Recording and Storage: Records student scores, validates that scores fall within the allowed range, and stores scores by student ID.

Average Calculation: Calculates and rounds the average score for each student.

Letter Grade Policy: Assigns letter grades based on academic grading thresholds.

Honor Roll Eligibility: Determines whether a student qualifies for the honor roll based on academic performance rules.

Transcript Formatting: Generates a plain-text transcript containing student information, average score, letter grade, and honor roll status.

CSV Export: Exports student IDs, averages, letter grades, and honor roll status in CSV format.

6-KitchenTicket

Order Item Management: Stores order items, their ingredients, and their preparation times.

Allergen Detection: Scans item ingredients and identifies potential allergens based on predefined ingredient-matching rules.

Preparation Time Estimation: Estimates when an order will be ready based on preparation times and the number of available kitchen stations.

Allergy Protocol Timing: Adds extra preparation time when allergens are detected to account for the allergy protocol.

Thermal Ticket Formatting: Generates a formatted kitchen ticket containing the order number, estimated preparation time, items, and allergen information.

Kitchen Routing Decisions: Determines the appropriate kitchen lane based on allergen detection and estimated preparation time.

7- LoanDesk

Risk Assessment: Calculates a loan applicant's risk score based on credit score, employment duration, requested amount, and collateral availability.

Loan Eligibility Decision: Determines whether an applicant qualifies for a loan based on risk score and credit score thresholds.

Required Documents Management: Determines which documents are required based on application details, eligibility, and compliance rules.

Decision Letter Generation: Generates applicant-facing letters communicating loan approval or rejection decisions.

Underwriter Data Export: Generates CSV-formatted application data for underwriter review and analytics.

8- SubscriptionBilling

Subscription Proration Calculation: Calculates subscription charges based on the active period and monthly subscription price.

Invoice Number Generation: Generates sequential invoice numbers using the billing period and a predefined numbering scheme.

Failed Payment Tracking: Tracks the number of failed payment attempts for a subscription.

Dunning Policy and Email Generation: Determines the appropriate payment reminder severity based on failed payment attempts and generates collection emails.

Accounting Ledger Export: Generates formatted accounting journal lines containing customer, invoice, amount, and account information.

9- SupportTicket

Ticket Data Management: Stores support ticket information and appends new customer messages to the ticket body.

Priority Classification: Determines ticket priority by analyzing subject and body text using keyword-based rules.

SLA Deadline Calculation: Calculates response deadlines based on ticket priority and service-level agreement rules.

SLA Breach Detection: Determines whether the ticket has exceeded its SLA deadline.

Public Reply Generation: Generates customer-facing replies using priority-dependent response wording and SLA deadline information.

Internal Escalation Message Generation: Generates internal escalation messages containing ticket ID, priority, SLA deadline, and keyword-scanning information.

10-WarehousePickList

Pick List Data Management: Stores requested items, warehouse locations, required quantities, and available stock quantities.

Stock Allocation: Calculates the quantity allocated for each SKU based on required quantities and available stock.

Warehouse Walking Order: Orders picking locations by aisle and bin to produce a warehouse picking sequence.

Picker Instructions Generation: Generates human-readable picking instructions for warehouse operators.

Shortage Detection and Reporting: Identifies items whose allocated quantities are lower than their required quantities and reports shortages.

WMS XML Generation: Generates XML batches containing allocated SKU quantities for integration with the Warehouse Management System.