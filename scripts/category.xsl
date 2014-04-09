<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_category')">
          <category>
            <categoryid m:left="-144" m:top="66" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </categoryid>
            <parentid m:left="-14" m:top="84" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="user:category(parentid)" />
            </parentid>
            <categoryname m:left="-144" m:top="102" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </categoryname>
            <description m:left="-14" m:top="118" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="description" />
            </description>
            <sequence m:left="-144" m:top="134" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="sequence" />
            </sequence>
            <inactive m:left="-80" m:top="186" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="inactived" />
            </inactive>
          </category>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
  <msxsl:script implements-prefix="user" language="CSharp"><![CDATA[
    public string category(string key)
    {  if (key == "0") return "NULL";
        return "fk:category(" + key + ")";
    }
]]></msxsl:script>
</xsl:stylesheet>