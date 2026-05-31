1)If database is leaked, attacker will get all access to all passwords in db.

2)Using plain SHA-256 can lead to "ranbow tables", which are precomputed collections of "password–hash"
pairs for popular algorithms. Attacker can look up hashes witout even computing everytihng.
Also 2 identical passwords would have identical hash.

3)Salt ensures that the same password for two different users produces different hashes.
It protects against precomputed rainbow tables and makes mass password cracking harder.

4)Unlike salt, we do not store pepper it in the database. It should reside outside the database(env, secure storage, e.g).

5)authentication - answers the question: who are you? (password + login check).
authorization - answers the question: what are you allowed to do? (check rights to perform action)

6)Because users can still manually enter the URL or inspect network requests.

7)Attackers can use this message to build a list of existing accounts and use it for further attacks

