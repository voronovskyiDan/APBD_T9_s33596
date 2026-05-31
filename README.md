1)Why must passwords not be stored as plain text?
If database is leaked, attacker will get all access to all passwords in db.

2)Why is raw SHA-256 not a good choice for passwords?
Using plain SHA-256 can lead to "ranbow tables", which are precomputed collections of "password–hash"
pairs for popular algorithms. Attacker can look up hashes witout even computing everytihng.
Also 2 identical passwords would have identical hash.

3)Why do we use salt?
Salt ensures that the same password for two different users produces different hashes.
It protects against precomputed rainbow tables and makes mass password cracking harder.

4)What is the difference between salt and pepper?
Unlike salt, we do not store pepper it in the database. It should reside outside the database(env, secure storage, e.g).

5)What is the difference between authentication and authorization?
authentication - answers the question: who are you? (password + login check)
authorization - answers the question: what are you allowed to do? (check rights to perform action)

6)Why is hiding a link in a view not enough as security?
Because users can still manually enter the URL or inspect network requests

7)Why can a "there is no such user" login message be a problem?
Attackers can use this message to build a list of existing accounts.

