using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace Viktorina_3
{
    public class sql
    {
        OleDbConnection connection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\viktorina.accdb"));

        public string isTeacher(string name)
        {
            OleDbCommand selectCommand = new OleDbCommand("SELECT * FROM users WHERE name='" +name+"'", connection);
            connection.Open();
            DataTable table = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = selectCommand;
            adapter.Fill(table);
            connection.Close();
            return table.Rows[0][1].ToString();
        }

        public void registration(string name, bool isTeacher)
        {
            OleDbCommand insertCommand = new OleDbCommand("INSERT INTO users ([name],[isTeacher]) VALUES (?,?)", connection);
            connection.Open();
            insertCommand.Parameters.AddWithValue(@"name", name);
            insertCommand.Parameters.AddWithValue(@"isTeacher", isTeacher);
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }
        public void insertQuestion(string question, string[] answer, bool[] isCorrect, string testName, int questionID)
        {
            OleDbCommand insertCommand = new OleDbCommand("INSERT INTO " + testName + " ([ID],[Question],[Ans1],[Cor1],[Ans2],[Cor2],[Ans3],[Cor3],[Ans4],[Cor4]) VALUES (?,?,?,?,?,?,?,?,?,?)", connection);
            connection.Open();
            insertCommand.Parameters.AddWithValue("@ID", questionID);
            insertCommand.Parameters.AddWithValue("@Question", question);
            insertCommand.Parameters.AddWithValue("@Ans1", answer[0]);
            insertCommand.Parameters.AddWithValue("@Cor1", isCorrect[0]);
            insertCommand.Parameters.AddWithValue("@Ans2", answer[1]);
            insertCommand.Parameters.AddWithValue("@Cor2", isCorrect[1]);
            insertCommand.Parameters.AddWithValue("@Ans3", answer[2]);
            insertCommand.Parameters.AddWithValue("@Cor3", isCorrect[2]);
            insertCommand.Parameters.AddWithValue("@Ans4", answer[3]);
            insertCommand.Parameters.AddWithValue("@Cor4", isCorrect[3]);
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void newTable(string tableName)
        {
            OleDbCommand newTable = new OleDbCommand("CREATE TABLE " + tableName + " (ID BYTE, Question MEMO, Ans1 MEMO, Cor1 BIT, Ans2 MEMO, Cor2 BIT, Ans3 MEMO, Cor3 BIT, Ans4 MEMO, Cor4 BIT)", connection);
            connection.Open();            
            newTable.ExecuteNonQuery();
            OleDbCommand insertCommand = new OleDbCommand("INSERT INTO tests ([testName]) VALUES (?)", connection);
            insertCommand.Parameters.AddWithValue("@testName", tableName);
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        public DataTable getTests()
        {
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT testName from tests where testName IS NOT NULL", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr;
            dr = dt.NewRow();
            dt.Rows.InsertAt(dr, 1);
            connection.Close();
            return dt;
        }

        public DataTable getResults(string testName)
        {
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from results where testName='"+testName+"'", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr;
            dr = dt.NewRow();
            dt.Rows.InsertAt(dr, 1);
            connection.Close();
            return dt;
        }

        public int maxID(string testName)
        {
            try { connection.Open(); }
            catch (Exception ex) { };
            OleDbCommand command = new OleDbCommand("SELECT MAX(ID) from " + testName, connection);
            int max = (int)command.ExecuteScalar();
            connection.Close();
            return max;
        }


        public DataTable fillQuestions(string testName, int questionID)
        {
            OleDbCommand selectCommand = new OleDbCommand("SELECT * FROM " + testName + " WHERE ID=" + questionID, connection);
            connection.Open();
            DataTable table = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = selectCommand;
            adapter.Fill(table);
            connection.Close();
            return table;
        }

        public void updateModifier(string question, string[] answer, bool[] isCorrect, string testName, int questionID)
        {
            OleDbCommand insertCommand = new OleDbCommand("UPDATE " + testName + " SET [Question] = ?, [Ans1] = ?, [Cor1] = ?, [Ans2] = ?, [Cor2] = ?, [Ans3] = ?, [Cor3] = ?, [Ans4] = ?, [Cor4] = ? WHERE [ID] =" +questionID, connection);
            connection.Open();
            insertCommand.Parameters.AddWithValue("@Question", question);
            insertCommand.Parameters.AddWithValue("@Ans1", answer[0]);
            insertCommand.Parameters.AddWithValue("@Cor1", isCorrect[0]);
            insertCommand.Parameters.AddWithValue("@Ans2", answer[1]);
            insertCommand.Parameters.AddWithValue("@Cor2", isCorrect[1]);
            insertCommand.Parameters.AddWithValue("@Ans3", answer[2]);
            insertCommand.Parameters.AddWithValue("@Cor3", isCorrect[2]);
            insertCommand.Parameters.AddWithValue("@Ans4", answer[3]);
            insertCommand.Parameters.AddWithValue("@Cor4", isCorrect[3]);
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void deleteTable(string tableName)
        {
            OleDbCommand deleteTable = new OleDbCommand("DROP TABLE "+tableName, connection);
            OleDbCommand clearFromTests = new OleDbCommand("DELETE testName FROM tests WHERE testName ='" + tableName + "'", connection);
            connection.Open();
            deleteTable.ExecuteNonQuery();
            clearFromTests.ExecuteNonQuery();
            connection.Close();
        }

        public void insertInResults(string studentName, string testName, int correct, int outOf)
        {
            OleDbCommand insertInResults = new OleDbCommand("INSERT INTO results ([studentName], [testName], [correct], [outOf]) VALUES (?,?,?,?)", connection);
            connection.Open();
            insertInResults.Parameters.Add(@"studentName", studentName);
            insertInResults.Parameters.Add(@"testName", testName);
            insertInResults.Parameters.Add(@"correct", correct);
            insertInResults.Parameters.Add(@"outOf", outOf);
            insertInResults.ExecuteNonQuery();
            connection.Close();
        }

        public DataTable getResultsStudent(string testName, string studentName)
        {
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from results where testName='" + testName + "' AND studentName='"+studentName+"'", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr;
            dr = dt.NewRow();
            dt.Rows.InsertAt(dr, 1);
            connection.Close();
            return dt;
        }
    }
}
