describe('Login Form Validation', () => {
  it('should validate a valid login form', () => {
    cy.visit('http://localhost:3000/login');
    cy.get('input[name="email"]').type('invalid_email');
    cy.get('input[name="password"]').type('weak');
    cy.get('button[type="submit"]').click();

    // Validate error messages
    cy.contains('.text-sm.font-medium.text-destructive', 'Invalid email').should('be.visible');
    cy.contains('.text-sm.font-medium.text-destructive', 'String must contain at least 6 character(s)').should(
      'be.visible',
    );
  });

  it('should submit the form for valid input', () => {
    cy.visit('http://localhost:3000/login');

    // Interact with form elements and submit
    cy.get('input[name="email"]').type('john.doe@example.com');
    cy.get('input[name="password"]').type('strongpassword');
    cy.get('button[type="submit"]').click();

    // Validate that the form is submitted successfully
    // Add your assertions here
  });
});
