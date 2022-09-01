CREATE SCHEMA ppp;

CREATE TABLE ppp.palindromics (
    index bigint NOT NULL,
    length integer NOT NULL,
    digits text NOT NULL,
    is_prime boolean NOT NULL,
    CONSTRAINT pk_palindromics PRIMARY KEY (index),
    CONSTRAINT digits_length CHECK (length(digits) = length)
);

CREATE TABLE ppp.pi (
    million integer NOT NULL,
    digits text NOT NULL,
    CONSTRAINT pk_pi PRIMARY KEY (million),
    CONSTRAINT digits_length CHECK (length(digits) = 1000000)
);
