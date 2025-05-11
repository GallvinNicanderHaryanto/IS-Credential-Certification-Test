CREATE DATABASE dbd_LPSlibrary;
USE dbd_LPSlibrary;

DROP TABLE BUKU;

TRUNCATE BUKU;

CREATE TABLE BUKU (
    ID_BUKU INT AUTO_INCREMENT,
    JUDUL_BUKU VARCHAR(45) NOT NULL,
    KATEGORI VARCHAR(20) NOT NULL,
    PENERBIT VARCHAR(15) NOT NULL,
    PENULIS VARCHAR(30) NOT NULL,
    DELETE_BUKU INT NOT NULL,
    PRIMARY KEY (ID_BUKU)
);

ALTER TABLE BUKU 
ADD CONSTRAINT FK_BUKU_PENERBIT 
FOREIGN KEY (KODE_PENERBIT) 
REFERENCES PENERBIT (KODE_PENERBIT) 
ON DELETE RESTRICT 
ON UPDATE RESTRICT;

INSERT INTO BUKU (JUDUL_BUKU, KATEGORI, PENERBIT, PENULIS, DELETE_BUKU) 
VALUES
    ('Laskar Pelangi', 'Fiksi', 'ABC', 'Andrea Hirata', '0'),
    ('Sang Pemimpi', 'Fiksi', 'BCD', 'Andrea Hirata', '0'),
    ('Twivortiare', 'Romansa', 'EFG', 'Ika Natassa', '0'),
    ('Critical Eleven', 'Romansa', 'HIJ', 'Ika Natassa', '0'),
    ('Susah Sinyal', 'Komedi', 'KLM', 'Ika Natassa', '0'),
    ('Marmut Merah Jambu', 'Komedi', 'NOP', 'Raditya Dika', '0'),
    ('Manusia Setengah Salmon', 'Komedi', 'PQR', 'Raditya Dika', '0'),
    ('The Christmas Pig', 'Dongeng', 'STJ', 'J.K. Rowling', '0'),
    ('Anak Semua Bangsa', 'Sejarah', 'VTH', 'Pramoedya Ananta Toer', '0'),
    ('Arok Dedes', 'Sejarah', 'ZTH', 'Pramoedya Ananta Toer', '0');


select * from buku;

drop table MAHASISWA;

CREATE TABLE MAHASISWA
(
	ID_MAHASISWA INT AUTO_INCREMENT PRIMARY KEY,
    NIM_MAHASISWA varchar(15) NOT NULL,
    NAMA_MAHASISWA varchar(35) NOT NULL,
    JENIS_KELAMIN_MAHASISWA char(1) NOT NULL,
    TELEPON_MAHASISWA varchar(13) NOT NULL,
    ALAMAT_MAHASISWA varchar(50),
    EMAIL_MAHASISWA varchar(25) NOT NULL,
    DELETE_MAHASISWA INT NOT NULL
);

INSERT INTO MAHASISWA (NIM_MAHASISWA, NAMA_MAHASISWA, JENIS_KELAMIN_MAHASISWA, TELEPON_MAHASISWA, ALAMAT_MAHASISWA, EMAIL_MAHASISWA, DELETE_MAHASISWA)
VALUES
('20240901', 'Merlin', 'F', '081309120112', 'Jl. Merpati', 'merlin@gmail.com', '0'),
('20240902', 'Jenni', 'F', '081309111178', 'Jl. Garuda', 'jenni@gmail.com', '0'),
('20240903', 'Apin', 'M', '081109122762', 'Jl. Elang', 'apin@gmail.com', '0'),
('20240904', 'Budi', 'M', '081234567890', 'Jl. Kenari', 'budi@gmail.com', '0'),
('20240905', 'Siti', 'F', '081234567891', 'Jl. Kutilang', 'siti@gmail.com', '0'),
('20240906', 'Andi', 'M', '081234567892', 'Jl. Merpati', 'andi@gmail.com', '0'),
('20240907', 'Dewi', 'F', '081234567893', 'Jl. Cendrawasih', 'dewi@gmail.com', '0'),
('20240908', 'Rudi', 'M', '081234567894', 'Jl. Angsa', 'rudi@gmail.com', '0'),
('20240909', 'Lina', 'F', '081234567895', 'Jl. Rajawali', 'lina@gmail.com', '0'),
('20240910', 'Fajar', 'M', '081234567896', 'Jl. Merpati', 'fajar@gmail.com', '0');

select * from mahasiswa;

DROP TABLE PEMINJAMAN;

CREATE TABLE peminjaman (
    id_pinjam INT AUTO_INCREMENT PRIMARY KEY,
    id_buku INT,
    id_mahasiswa INT,
    nama_mahasiswa VARCHAR(100) NOT NULL,
    judul_buku VARCHAR(255) NOT NULL,
    tanggal_pinjam DATE NOT NULL,
    tanggal_pengembalian DATE NOT NULL,
    FOREIGN KEY (ID_BUKU) REFERENCES BUKU(ID_BUKU),
    FOREIGN KEY (ID_MAHASISWA) REFERENCES MAHASISWA(ID_MAHASISWA)
);

select * from peminjaman;