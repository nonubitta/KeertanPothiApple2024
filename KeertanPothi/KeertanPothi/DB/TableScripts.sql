CREATE TABLE Pothi (
    POTHIID     INTEGER PRIMARY KEY,
    NAME        VARCHAR2(20),
    CREATEDON   DATE
);

CREATE TABLE PothiShabad (
 	PothiId     INT NOT NULL,
    Shabadid    INT NOT NULL,
  	VerseId		INT,
    SortOrder   INT,
  PRIMARY KEY (PothiId, Shabadid)
);

CREATE TABLE History (
  ShabadId integer PRIMARY key,
  CreatedOn Date
  );

CREATE TABLE WriterInfo (
  	writerid	integer	UNIQUE,
  	Birthdt		Date,
  	BirthPlace	varchar2(100),
  	DeathDt		Date,
  	Spouse		varchar2(100),
  	Children	varchar2(200),
  	Parents		varchar2(200),
    IsGuru		bool,
  	Info		varchar2(1000)
  );

  CREATE TABLE History (
  ShabadId integer PRIMARY key,
  VerseId Integer,
  CreatedOn Date
);

   INSERT INTO WriterInfo values (1, '15-Apr-1469', 'Rai Bhoi Ki Talvandi, Nankana Sahib, Punjab, Pakistan', '22-Sep-1539', 'Mata Sulakhani ji', 
                                  'Sri Chand, Lakhmi Das', 'Mehta Kalu and Mata Tripta', true,
'');


   INSERT INTO WriterInfo values (2, '31-Mar-1504', 'Matte Di Sarai, Muktsar, Punjab, India', '29-Mar-1552', 'Mata Khivi', 'Baba Dasu, Baba Dattu, Bibi Amro, and Bibi Anokhi', 'Mata Ramo and Baba Pheru Mal', true, 
'');


   INSERT INTO WriterInfo values (3, '5-May-1479',   --dob
                                  'Basarke,Punjab India', --birthplace
                                  '1-Sep-1574', --death
                                  'Mata Mansa Devi', --Spouse
                                  'Bhai Mohan, Bhai Mohri, Bibi Dani, and Bibi Bhani', --Children
                                  'Tej Bhan & Mata Lachmi', --parents
                                  true, --isguru
								 ''
                                 );

								 
   INSERT INTO WriterInfo values (4, '24-Sep-1534',   --dob  
                                  'Chuna Mandi, Lahore, Punjab, Pakistan', --birthplace
                                  '1-Sep-1581', --death tember 1, 
                                  'Bibi Bhani ji', --Spouse
                                  'Prithi Chand, Mahan Dev, and Guru Arjan', --Children
                                  'Hari Das and Mata Daya', 
                                  true, 
								 ''
                                 );

   INSERT INTO WriterInfo values (5, '15-Apr-1563',   --dob 
                                  'Goindval, Taran Taran, India', --birthplace
                                  '30-May-1606', --death    
                                  'Mata Ganga ji', --Spouse
                                  'Guru Hargobind', --Children
                                  'Guru Ram Das and Mata Bhani ji', 
                                  true, 
								 ''
                                 );
								 

   --INSERT INTO WriterInfo values (6, '19-Jun-1595',   --dob  
   --                               'Guru Ki Wadali, Amritsar, India', --birthplace
   --                               '3-Mar-1644', --death  
   --                               'Damodari,Nanaki,Mahadevi', --Spouse
   --                               'Baba Gurdita, Baba Suraj Mal, Baba Ani Rai, Baba Atal Rai, Guru Tegh Bahadur, and Bibi Veero', --Children
   --                               'Guru Arjun Dev and Mata Ganga', 
   --                               true, 
			--					 ''
   --                              );
								 

   --INSERT INTO WriterInfo values (7, '16-Jan-1630',   --dob 
   --                               'Kiratpur Sahib, Rupnagar, Punjab, India', --birthplace
   --                               '6-Oct-1661', --death 
   --                               'Mata Krishen Devi', --Spouse
   --                               'Baba Ram Rai and Guru Har Krishan', --Children
   --                               'Baba Gurditta ji,Mata Nihal Kaur', 
   --                               true, 
			--					 ''
   --                              );
								 

   --INSERT INTO WriterInfo values (8, '7-Jul-1656',   --dob 
   --                               'Kiratpur, Sivalik Hills, India', --birthplace
   --                               '30-Mar-1664', --death  , 
   --                               '', --Spouse
   --                               '', --Children
   --                               'Guru Har Rai ji,Mata Krishen', 
   --                               true, 
			--					 ''
   --                              );
								 
								

                                --Gur tegh bahadur ji
   INSERT INTO WriterInfo values (6, '1-Apr-1621',   --dob 
                                  'Amritsar, Punjab, India', --birthplace
                                  '11-Nov-1675', --death  , 
                                  '	Mata Gujri', --Spouse
                                  'Guru Gobind Singh', --Children
                                  'Guru Hargobind sahib ji, Mata Nanaki', 
                                  true, 
								 ''
                                 );

                                 --Guru gobind singh ji
   INSERT INTO WriterInfo values (47, '22-Dec-1666',   --dob  
                                  'Patna, Bihar, India', --birthplace
                                  '7-Oct-1708', --death  
                                  'Mata Jito, Mata Sundari, Mata Sahib Devan', --Spouse
                                  'Ajit Singh, Jujhar Singh, Zorawar Singh, Fateh Singh', --Children
                                  'Guru Tegh Bahadur, Mata Gujri ji', 
                                  true, 
								 ''
                                 );
								
								 