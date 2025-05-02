-- Authors
INSERT INTO Authors (Name) VALUES
('George Orwell'),
('Jane Austen'),
('Mark Twain'),
('J.K. Rowling'),
('Agatha Christie'),
('Leo Tolstoy'),
('Ernest Hemingway'),
('F. Scott Fitzgerald'),
('Virginia Woolf'),
('Charles Dickens'),
('Harper Lee'),
('J.R.R. Tolkien'),
('C.S. Lewis'),
('Isaac Asimov'),
('Arthur C. Clarke');

-- Categories
INSERT INTO Categories (Name) VALUES
('Fiction'),
('Science Fiction'),
('Mystery'),
('Fantasy'),
('Non-Fiction'),
('Biography'),
('History'),
('Romance');

-- Books
INSERT INTO Books (Title, AuthorID, CategoryID, ISBN, PublishDate) VALUES
('1984', 1, 2, '9780451524935', '1949-06-08'),
('Pride and Prejudice', 2, 1, '9780141439518', '1813-01-28'),
('Adventures of Huckleberry Finn', 3, 1, '9780486280615', '1884-12-10'),
('Harry Potter and the Sorcerer''s Stone', 4, 4, '9780590353427', '1997-06-26'),
('Murder on the Orient Express', 5, 3, '9780062693662', '1934-01-01'),
('War and Peace', 6, 5, '9780199232765', '1869-01-01'),
('The Old Man and the Sea', 7, 1, '9780684801223', '1952-09-01'),
('The Great Gatsby', 8, 1, '9780743273565', '1925-04-10'),
('Mrs. Dalloway', 9, 1, '9780156628709', '1925-05-14'),
('A Tale of Two Cities', 10, 5, '9780486406510', '1859-04-30'),
('To Kill a Mockingbird', 11, 1, '9780061120084', '1960-07-11'),
('The Fellowship of the Ring', 12, 4, '9780547928210', '1954-07-29'),
('The Lion, the Witch and the Wardrobe', 13, 4, '9780064471046', '1950-10-16'),
('Foundation', 14, 2, '9780553293357', '1951-06-01'),
('2001: A Space Odyssey', 15, 2, '9780451457998', '1968-07-01');

-- Members
INSERT INTO Members (FullName, Email, Phone, JoinDate) VALUES
('Alice Johnson', 'alice.johnson@example.com', '555-555-1234', '2023-09-15'),
('Bob Smith', 'bob.smith@example.com', '555-555-5678', '2024-01-10'),
('Carol Lee', 'carol.lee@example.com', '555-555-8765', '2024-03-22'),
('David Kim', 'david.kim@example.com', '555-555-4321', '2024-08-05'),
('Eva Brown', 'eva.brown@example.com', '555-555-2468', '2025-02-14'),
('Frank Green', 'frank.green@example.com', '555-555-1357', '2023-11-20'),
('Grace Hall', 'grace.hall@example.com', '555-555-2469', '2024-02-18'),
('Henry Adams', 'henry.adams@example.com', '555-555-9753', '2024-05-30'),
('Isabella Moore', 'isabella.moore@example.com', '555-555-8642', '2024-07-12'),
('Jack Wilson', 'jack.wilson@example.com', '555-555-3141', '2024-09-01'),
('Karen Thomas', 'karen.thomas@example.com', '555-555-2718', '2025-01-05'),
('Liam Scott', 'liam.scott@example.com', '555-555-1618', '2025-02-28'),
('Mia Turner', 'mia.turner@example.com', '555-555-3142', '2025-03-15'),
('Noah Carter', 'noah.carter@example.com', '555-555-4242', '2025-03-30'),
('Olivia Perez', 'olivia.perez@example.com', '555-555-5252', '2025-04-10');

-- Loans (20 entries)
INSERT INTO Loans (BookID, MemberID, CheckoutDate, DueDate, ReturnDate) VALUES
(1, 1, '2025-04-01', '2025-04-15', NULL),
(2, 2, '2025-04-05', '2025-04-19', '2025-04-18'),
(3, 3, '2025-04-07', '2025-04-21', '2025-04-20'),
(4, 4, '2025-04-08', '2025-04-22', NULL),
(5, 5, '2025-04-10', '2025-04-24', NULL),
(6, 6, '2025-04-12', '2025-04-26', '2025-04-25'),
(7, 7, '2025-04-14', '2025-04-28', NULL),
(8, 8, '2025-04-16', '2025-04-30', NULL),
(9, 9, '2025-04-02', '2025-04-16', '2025-04-15'),
(10, 10, '2025-04-03', '2025-04-17', NULL),
(11, 11, '2025-04-04', '2025-04-18', '2025-04-17'),
(12, 12, '2025-04-05', '2025-04-19', NULL),
(13, 13, '2025-04-06', '2025-04-20', NULL),
(14, 14, '2025-04-07', '2025-04-21', '2025-04-20'),
(15, 15, '2025-04-08', '2025-04-22', NULL),
(1, 6, '2025-04-09', '2025-04-23', '2025-04-22'),
(2, 7, '2025-04-10', '2025-04-24', NULL),
(3, 8, '2025-04-11', '2025-04-25', '2025-04-24'),
(4, 9, '2025-04-12', '2025-04-26', NULL),
(5, 10, '2025-04-13', '2025-05-20', NULL);

-- Fines (for overdue loans)
INSERT INTO Fines (LoanID, Amount, Paid) VALUES
(1, 14 * 0.50, 0),
(4, 6 * 0.50, 0),
(5, 4 * 0.50, 0),
(7, 1 * 0.50, 0),
(9, 13 * 0.50, 0),
(10, 12 * 0.50, 0),
(12, 10 * 0.50, 0),
(13, 9 * 0.50, 1);