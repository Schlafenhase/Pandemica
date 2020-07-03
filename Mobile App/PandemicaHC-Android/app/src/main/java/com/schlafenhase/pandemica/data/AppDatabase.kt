package com.schlafenhase.pandemica.data

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import androidx.sqlite.db.SupportSQLiteDatabase
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch
import java.util.prefs.AbstractPreferences

@Database(entities = [Patient::class, Contact::class], version = 1, exportSchema = true)
abstract class AppDatabase : RoomDatabase() {

    abstract fun patientDao(): PatientDAO
    abstract fun contactDao(): ContactDAO

    companion object {
        // Singleton prevents multiple instances of database opening at the same time.
        @Volatile
        private var INSTANCE: AppDatabase? = null

        fun getDatabase(context: Context,
                        scope: CoroutineScope
        ): AppDatabase {
            val tempInstance = INSTANCE
            if (tempInstance != null) {
                // Database exists
                return tempInstance
            }
            synchronized(this) {
                // Create database
                val instance = Room.databaseBuilder(
                    context.applicationContext,
                    AppDatabase::class.java,
                    "PandemicaDBLite_Android"
                )
                    .addCallback(AppDatabaseCallback(scope))
                    .build()
                INSTANCE = instance
                return instance
            }
        }
    }

    private class AppDatabaseCallback(
        private val scope: CoroutineScope
    ) : RoomDatabase.Callback() {

        override fun onCreate(db: SupportSQLiteDatabase) {
            super.onOpen(db)
            INSTANCE?.let { database ->
                scope.launch {
                    populateDatabase(database.patientDao(), database.contactDao())
                }
            }
        }

        /**
         * Initial database population
         */
        suspend fun populateDatabase(patientDAO: PatientDAO, contactDAO: ContactDAO) {
            // Delete all content here.
            patientDAO.deleteAll()
            contactDAO.deleteAll()

            // Add initial patients.
            var p1 = Patient(
                name = "Alejandro",
                lastName = "Ibarra",
                patientID = 117890851,
                age = 19,
                nationality = "Costa Rica",
                region = "The Americas",
                pathologies = "sick burns, tooth pain",
                status = "Alive",
                medication = "Aderall",
                isHospitalized = true,
                isInUCI = false
            )
            var p2 = Patient(
                name = "Jose Daniel",
                lastName = "Acuña",
                patientID = 118920182,
                age = 20,
                nationality = "Costa Rica",
                region = "The Americas",
                pathologies = "real sick",
                status = "Dead",
                medication = "The Blue Pill",
                isHospitalized = false,
                isInUCI = false
            )
            patientDAO.insert(p1)
            patientDAO.insert(p2)

            // Add initial contacts.
            var c1 = Contact(
                name = "Marta",
                lastName = "Cordero",
                contactID = 1,
                age = 20,
                nationality = "Costa Rica",
                address = "San Ramon",
                pathologies = "nothing",
                email = "kscorzu@gmail.com",
                assignedTo = 1
            )
            var c2 = Contact(
                name = "Jesus",
                lastName = "Sandoval",
                contactID = 2,
                age = 20,
                nationality = "Costa Rica",
                address = "Orotina",
                pathologies = "nothing",
                email = "jose.ibarra@hotmail.com",
                assignedTo = 1
            )
            var c3 = Contact(
                name = "Paula",
                lastName = "Álvarez",
                contactID = 3,
                age = 64,
                nationality = "Costa Rica",
                address = "Grecia",
                pathologies = "nothing",
                email = "hsalas@hotmail.com",
                assignedTo = 1
            )
            var c4 = Contact(
                name = "Pablo",
                lastName = "Alvarado",
                contactID = 4,
                age = 54,
                nationality = "Costa Rica",
                address = "Sabanilla",
                pathologies = "nothing",
                email = "ccampos@hotmail.com",
                assignedTo = 1
            )
            var c5 = Contact(
                name = "Jessenia",
                lastName = "Salas",
                contactID = 5,
                age = 20,
                nationality = "Costa Rica",
                address = "Santa Gertrudis",
                pathologies = "nothing",
                email = "raquesol@hotmail.com",
                assignedTo = 1
            )

            var c6 = Contact(
                name = "Quebin",
                lastName = "Cordero",
                contactID = 1,
                age = 20,
                nationality = "Costa Rica",
                address = "San Ramon",
                pathologies = "nothing",
                email = "kscorzu@gmail.com",
                assignedTo = 2
            )
            var c7 = Contact(
                name = "Jesus",
                lastName = "Sandoval",
                contactID = 2,
                age = 20,
                nationality = "Costa Rica",
                address = "Orotina",
                pathologies = "nothing",
                email = "jose.ibarra@hotmail.com",
                assignedTo = 2
            )
            var c8 = Contact(
                name = "Hannia",
                lastName = "Salas",
                contactID = 3,
                age = 64,
                nationality = "Costa Rica",
                address = "Grecia",
                pathologies = "nothing",
                email = "hsalas@hotmail.com",
                assignedTo = 2
            )
            var c9 = Contact(
                name = "Carlos",
                lastName = "Campos",
                contactID = 4,
                age = 54,
                nationality = "Costa Rica",
                address = "Sabanilla",
                pathologies = "nothing",
                email = "ccampos@hotmail.com",
                assignedTo = 2
            )
            var c10 = Contact(
                name = "Raquel",
                lastName = "Quesada",
                contactID = 5,
                age = 20,
                nationality = "Costa Rica",
                address = "Santa Gertrudis",
                pathologies = "nothing",
                email = "raquesol@hotmail.com",
                assignedTo = 2
            )

            contactDAO.insert(c1)
            contactDAO.insert(c2)
            contactDAO.insert(c3)
            contactDAO.insert(c4)
            contactDAO.insert(c5)
            contactDAO.insert(c6)
            contactDAO.insert(c7)
            contactDAO.insert(c8)
            contactDAO.insert(c9)
            contactDAO.insert(c10)
        }
    }
}